import React, { useState, useEffect } from "react";
import axios from "axios";

function ProteinList({proteins, setProteins}) {
    const [editId, setEditId] = useState(null);
    const [editedProtein, setEditedProtein] = useState(null);
    const [loading, setLoading] = useState(true);
    const [filter, setFilter] = useState({ flavor: '', minPrice: '', maxPrice: '', minWeight: '', maxWeight: '' });
    const [sorting, setSorting] = useState({ sortBy: 'Price', sortOrder: 'ASC' });
    const [paging, setPaging] = useState({ pageNumber: 1, pageSize: 5, totalPages: 0});     

    const fetchData = async () => {
        try {
            setLoading(true);
            const response = await axios.get('https://localhost:44371//Api/Protein', {
                params: {
                    flavor: filter.flavor,
                    minPrice: filter.minPrice,
                    maxPrice: filter.maxPrice,
                    minWeight: filter.minWeight,
                    maxWeight: filter.maxWeight,
                    sortBy: sorting.sortBy,
                    sortOrder: sorting.sortOrder,
                    pageNumber: paging.pageNumber,
                    pageSize: paging.pageSize
                }
            });
            setProteins(response.data.list);
            setPaging({ ...paging, totalPages: response.data.pageCount });
        } catch (error) {
            console.error('Error fetching data: ', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        const delay = setTimeout(() => {
            fetchData();
        }, 500);
        return () => clearTimeout(delay);
    }, [filter, sorting, paging.pageNumber]);

    
    const handleDelete = async (id) => {
        let deleteConfirmed = window.confirm("Are you sure you want to delete this protein?");
    
        if (deleteConfirmed) {
            try {
                await axios.delete(`https://localhost:44371//Api/Protein/${id}`);
                //const updatedProteins = proteins.filter(protein => protein.id !== id);
                //setProteins(updatedProteins);
                fetchData();
                alert("Protein deleted successfully!");
            } catch (error) {
                console.error("Error deleting protein: ", error);
            }
        } else {
            console.log("Delete canceled.");
        }
    }
    
    const handleEdit = (id) => {
        setEditId(id);
        const proteinToEdit = proteins.find(protein => protein.id === id);
        setEditedProtein(proteinToEdit);
    }

    const handleEditSubmit = async (e) => {
        e.preventDefault();
        const flavor = e.target.flavor.value;
        const price = e.target.price.value;
        const weight = e.target.weight.value;
        const category = e.target.category.value;

        try {
            await axios.put(`https://localhost:44371//Api/Protein/${editId}`, { flavor, price, weight, category });
            const updatedProteins = proteins.map(protein => {
                if (protein.id === editId) {
                    return { ...protein, flavor, price, weight, category };
                }
                return protein;
            });
            setProteins(updatedProteins);
            alert("Protein edited successfully!");
            setEditId(null);
        } catch (error) {
            console.error("Error editing protein: ", error);
        }
    }

    const handleFilterChange = (e) => {
        setFilter({ ...filter, [e.target.name]: e.target.value });
    };

    const handleSortChange = (sortBy) => {
        setSorting({ sortBy, sortOrder: sorting.sortOrder === 'asc' ? 'desc' : 'asc' });
    };

    const handlePageChange = (number) => {
        console.log(paging.totalPages);
        if (number < 1 || number> paging.totalPages) {
            return;
        }
        setPaging({ ...paging, pageNumber: number });
    };

    return (
        <>
        <div className="container">

            <div className="filters">
                <label htmlFor="flavor">Flavor:</label>
                <input className="filterInput" type="text" name="flavor" onChange={handleFilterChange}/>
                {/*   
                <select id="flavor" name="flavor" value={filter.flavor} onChange={handleFilterChange}>
                    <option value="">All</option>
                    {proteins.map((protein, index) => (
                        <option key={index} value={protein.flavor}>{protein.flavor}</option>
                    ))}
                </select>
                */}

                <label htmlFor="minPrice">Min price(€):</label>
                <input className="filterInput" type="number" id="minPrice" name="minPrice" value={filter.minPrice} onChange={handleFilterChange} />
                
                <label htmlFor="maxPrice">Max price(€):</label>
                <input className="filterInput" type="number" id="maxPrice" name="maxPrice" value={filter.maxPrice} onChange={handleFilterChange} />
                
                <label htmlFor="minWeight">Min weight:</label>
                <input className="filterInput" type="number" id="minWeight" name="minWeight" value={filter.minWeight} onChange={handleFilterChange} />
                
                <label htmlFor="maxWeight">Max weight:</label>
                <input className="filterInput" type="number" id="maxWeight" name="maxWeight" value={filter.maxWeight} onChange={handleFilterChange} />
                
                <button className="formButton" onClick={() => setFilter({ flavor: '', minPrice: '', maxPrice: '', minWeight: '', maxWeight: '' })}>Clear</button>   
                
            </div>

            <div className="sorting">

                <div className="sortButtons">
                    <button className="formButton" onClick={() => handleSortChange('Flavor')}>Sort by flavor</button>
                    <button className="formButton" onClick={() => handleSortChange('Price')}>Sort by price</button>
                    <button className="formButton" onClick={() => handleSortChange('Weight')}>Sort by weight</button>
                </div>

                <div className="table">
                    <table>
                        <thead>
                            <tr className="thead">
                                <th>Flavor</th>
                                <th>Price (€)</th>
                                <th>Weight</th>
                                <th>Category</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody className="tableBody">
                            {proteins.map((protein, index) => (
                                <tr key={index}>
                                    <td>{protein.flavor}</td>
                                    <td>{protein.price}</td>
                                    <td>{protein.weight}</td>
                                    <td>
                                        {protein.isAnabolic && <span>Anabolic </span>}
                                        {protein.isVegan && <span>Vegan </span>}
                                        {protein.isRecovery && <span>Recovery </span>}   
                                    </td>
                                    <td>
                                        {editId !== protein.id ? (
                                            <>
                                                <button className="formButton" onClick={() => handleEdit(protein.id)}>Edit</button>
                                                <button className="formButton" onClick={() => handleDelete(protein.id)}>Delete</button>
                                            </>
                                        ) : (
                                            <form onSubmit={handleEditSubmit} className="editForm">
                                                <label htmlFor="flavor">Flavor</label>
                                                <input type="text" id="flavor" name="flavor" defaultValue={editedProtein.flavor} /><br /><br />
                                                
                                                <label htmlFor="price">Price(€):</label>
                                                <input type="text" id="price" name="price" defaultValue={editedProtein.price} /><br /><br />
                                                
                                                <label htmlFor="weight">Weight</label>
                                                <input type="number" id="weight" name="weight" defaultValue={editedProtein.weight} /><br /><br />    
                                                
                                                <label htmlFor="category">Category:</label>
                                                <select id="category" name="category" defaultValue={editedProtein.category}>
                                                    <option value="Anabolic">Anabolic</option>
                                                    <option value="Vegan">Vegan</option>
                                                    <option value="Recovery">Recovery</option>
                                                </select>
                                                
                                                <br /><br />

                                                <input type="submit" value="Save" className="formButton" />
                                            </form>
                                        )}
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>

            </div>
        </div>

        <div className="pagination">
            <button className="pagingBtn" onClick={() => handlePageChange(paging.pageNumber - 1)} disabled={paging.pageNumber === 1}>{'<<'}</button>
            <span>{paging.pageNumber}</span>
            <button className="pagingBtn" onClick={() => handlePageChange(paging.pageNumber + 1)} disabled={paging.pageNumber === paging.totalPages}>{'>>'}</button>
        </div>
        </>
    );
}

export default ProteinList;
