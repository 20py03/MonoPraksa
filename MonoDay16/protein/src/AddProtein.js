import React, { useState, useEffect } from 'react';
import axios from 'axios';

function AddProtein({setProteins}) {
    const [formData, setFormData] = useState({
        flavor: '',
        price: '',
        weight: '',
        selectedCategory: '',
        categories: []
    });

    const fetchCategories = async () => {
        try {
            const response = await axios.get("https://localhost:44371/Api/Protein/Categories");
            setFormData(prevFormData => ({...prevFormData, categories: response.data}));
        } catch (error) {
            console.error("Error fetching categories: ", error);
        }
    }

    useEffect(() => {
        fetchCategories();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const { flavor, price, weight, selectedCategory } = formData;

        if (parseFloat(price) < 0) {
            alert("Price should be a positive number");
            return;
        }

        const protein = { flavor, price, weight, categoryId: selectedCategory };

        try {
            await axios.post("https://localhost:44371/Api/Protein", protein);
            setProteins(prevProteins => [...prevProteins, protein]);
            alert("Protein added successfully!");
            setFormData({
                flavor: '',
                price: '',
                weight: '',
                selectedCategory: ''
            })
        } catch (error) {
            console.error("Error adding protein: ", error);
        }
    }

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevFormData => ({...prevFormData, [name]: value}));
    }

    return (
        <form onSubmit={handleSubmit} className="addForm">
            <label htmlFor="flavor">Flavor:</label>
            <input type="text" id="flavor" name="flavor" required value={formData.flavor} onChange={handleInputChange}/><br /><br />

            <label htmlFor="price">Price(€):</label>
            <input type="text" id="price" name="price" min="0" required value={formData.price}  onChange={handleInputChange} /><br /><br />

            <label htmlFor="weight">Weight:</label>
            <input type="number" id="weight" name="weight" min="0" required value={formData.weight} onChange={handleInputChange} /><br /><br />

            <label htmlFor="category">Category:</label>
            <select id="categoryId" name="selectedCategory" required value={formData.selectedCategory} onChange={handleInputChange}>
                <option value="">Select category</option>
                {formData.categories && formData.categories.map(category => (
                    <option key={category.id} value={category.id}>{category.name}</option>
                ))}
            </select>
            <br /><br />
            
            <input type="submit" value="Add protein" className="addProtein" />
        </form>
    );
}

export default AddProtein;
