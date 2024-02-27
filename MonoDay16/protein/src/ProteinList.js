import React, { useState, useEffect } from "react";

function ProteinList({proteins, setProteins}) {
    const [editId, setEditId] = useState(null);
    const [editedProtein, setEditedProtein] = useState(null);

    useEffect(() => {
        const storedProteins = JSON.parse(localStorage.getItem("proteins")) || [];
        setProteins(storedProteins);
    }, []);

    const handleDelete = (id) => {
        let deleteConfirmed = window.confirm("Are you sure you want to delete this protein?");
    
        if (deleteConfirmed) {
            const updatedProteins = proteins.filter(protein => protein.id !== id);
            localStorage.setItem("proteins", JSON.stringify(updatedProteins));
            setProteins(updatedProteins);
            alert("Protein deleted successfully!");
        } else {
            console.log("Delete canceled.");
        }
    }

    const handleEdit = (id) => {
        setEditId(id);
        const proteinToEdit = proteins.find(protein => protein.id === id);
        setEditedProtein(proteinToEdit);
    }

    const handleEditSubmit = (e) => {
        e.preventDefault();
        const name = e.target.name.value;
        const flavor = e.target.flavor.value;
        const price = e.target.price.value;
        const quantity = e.target.quantity.value;

        if (name === "" || flavor === "" || price === "" || quantity === "") {
            alert("Enter all fields.");
            return;
        }

        const updatedProteins = proteins.map(protein => {
            if (protein.id === editId) {
                return { ...protein, name, flavor, price, quantity };
            }
            return protein;
        });

        localStorage.setItem("proteins", JSON.stringify(updatedProteins));
        setProteins(updatedProteins);
        alert("Protein edited successfully!");
        setEditId(null);
    }
    
    return (
        <div className="table">
            <table>
                <thead>
                    <tr className="thead">
                        <th>Name</th>
                        <th>Flavor</th>
                        <th>Price (€)</th>
                        <th>Quantity</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody className="tableBody">
                    {proteins.map((protein, index) => (
                        <tr key={index}>
                            <td>{protein.name}</td>
                            <td>{protein.flavor}</td>
                            <td>{protein.price}</td>
                            <td>{protein.quantity}</td>
                            <td>
                                {editId !== protein.id ? (
                                    <>
                                        <button className="formButton" onClick={() => handleEdit(protein.id)}>Edit</button>
                                        <button className="formButton" onClick={() => handleDelete(protein.id)}>Delete</button>
                                    </>
                                ) : (
                                    <form onSubmit={handleEditSubmit} className="editForm">
                                        <label htmlFor="name">Name:</label>
                                        <input type="text" id="name" name="name" defaultValue={editedProtein.name} /><br /><br />
                                        
                                        <label htmlFor="flavor">Flavor:</label>
                                        <input type="text" id="flavor" name="flavor" defaultValue={editedProtein.flavor} /><br /><br />
                                        
                                        <label htmlFor="price">Price:</label>
                                        <input type="number" id="price" name="price" defaultValue={editedProtein.price} /><br /><br />
                                        
                                        <label htmlFor="quantity">Quantity:</label>
                                        <input type="number" id="quantity" name="quantity" defaultValue={editedProtein.quantity} /><br /><br />
                                        
                                        <input type="submit" value="Save" className="formButton" />
                                    </form>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default ProteinList;
