import React, { useState } from 'react';

function AddProtein({setProteins}) {
    const [name, setName] = useState('');
    const [flavor, setFlavor] = useState('');
    const [price, setPrice] = useState('');
    const [quantity, setQuantity] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        const id = parseInt(localStorage.getItem("proteins")?.length || 0) + 1;
        const protein = { name, flavor, price, quantity, id };

        if (name === "" || flavor === "" || price === "" || quantity === "") {
            alert("Please fill all the fields");
            return;
        }

        if (price < 0 || quantity < 0) {
            alert("Price and Quantity should be a positive number");
            return;
        }

        let proteins = JSON.parse(localStorage.getItem("proteins")) || [];

        proteins.push(protein);
        localStorage.setItem("proteins", JSON.stringify(proteins));
        setProteins(proteins);
        alert("Protein added successfully!");
        setName('');
        setFlavor('');
        setPrice('');
        setQuantity('');
    }

    return (
        <form onSubmit={handleSubmit} className="addForm">
            <label htmlFor="name">Name:</label>
            <input type="text" id="name" name="name" value={name} onChange={(e) => setName(e.target.value)} /><br /><br />

            <label htmlFor="flavor">Flavor:</label>
            <input type="text" id="flavor" name="flavor" value={flavor} onChange={(e) => setFlavor(e.target.value)} /><br /><br />

            <label htmlFor="price">Price:</label>
            <input type="number" id="price" name="price" value={price} onChange={(e) => setPrice(e.target.value)} /><br /><br />

            <label htmlFor="quantity">Quantity:</label>
            <input type="number" id="quantity" name="quantity" value={quantity} onChange={(e) => setQuantity(e.target.value)} /><br /><br />

            <input type="submit" value="Add protein" className="addProtein" />
        </form>
    );
}

export default AddProtein;
