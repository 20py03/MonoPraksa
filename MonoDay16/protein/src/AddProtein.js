import React, { useState, useEffect } from 'react';
import axios from 'axios';

function AddProtein({setProteins}) {
    const [flavor, setFlavor] = useState('');
    const [price, setPrice] = useState('');
    const [weight, setWeight] = useState('');
    const [categories, setCategories] = useState([]);
    const [selectedCategory, setSelectedCategory] = useState('');
/*
    const handleSubmit = (e) => {
        e.preventDefault();
        const id = parseInt(localStorage.getItem("proteins")?.length || 0) + 1;
        const protein = { flavor, price, weight,category,id };

        if (flavor === "" || price === "" || weight === "") {
            alert("Please fill all the fields");
            return;
        }

        if (parseFloat(price) < 0) {
            alert("Price should be a positive number");
            return;
        }

        let proteins = JSON.parse(localStorage.getItem("proteins")) || [];

        proteins.push(protein);
        localStorage.setItem("proteins", JSON.stringify(proteins));
        setProteins(proteins);
        alert("Protein added successfully!");
        setFlavor('');
        setPrice('');
        setWeight('');
        setCategory('');
    }
*/
    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get("https://localhost:44371/Api/Protein/Categories");
                setCategories(response.data);
            } catch (error) {
                console.error("Error fetching categories: ", error);
            }
        }
        fetchCategories();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const protein = { flavor, price, weight, categoryId: selectedCategory };

        if (flavor === "" || price === "" || weight === "" || !selectedCategory) {
            alert("Please fill all the fields and select a category");
            return;
        }

        if (parseFloat(price) < 0) {
            alert("Price should be a positive number");
            return;
        }

        try {
            await axios.post("https://localhost:44371/Api/Protein", protein);
            setProteins(prevProteins => [...prevProteins, protein]);
            alert("Protein added successfully!");
            setFlavor('');
            setPrice('');
            setWeight('');
            setSelectedCategory('');
        } catch (error) {
            console.error("Error adding protein: ", error);
        }
    }

    return (
        <form onSubmit={handleSubmit} className="addForm">
            <label htmlFor="flavor">Flavor:</label>
            <input type="text" id="flavor" name="flavor" value={flavor} onChange={(e) => setFlavor(e.target.value)} /><br /><br />

            <label htmlFor="price">Price(€):</label>
            <input type="text" id="price" name="price" value={price} onChange={(e) => setPrice(e.target.value)} /><br /><br />

            <label htmlFor="weight">Weight:</label>
            <input type="number" id="weight" name="weight" value={weight} onChange={(e) => setWeight(e.target.value)} /><br /><br />

            <label htmlFor="category">Category:</label>
            <select id="categoryId" name="categoryId" value={selectedCategory} onChange={(e) => setSelectedCategory(e.target.value)}>
                <option value="">Select category</option>
                {categories.map(category => (
                    <option key={category.id} value={category.id}>{category.name}</option>
                ))}
            </select>
            <br /><br />
            
            <input type="submit" value="Add protein" className="addProtein" />
        </form>
    );
}

export default AddProtein;
