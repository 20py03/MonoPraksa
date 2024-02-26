import React from 'react';

function AddProtein() {
    const handleSubmit = (e) => {
        e.preventDefault();
        const name = e.target.name.value;
        const flavor = e.target.flavor.value;
        const price = e.target.price.value;
        const quantity = e.target.quantity.value;
        let proteins = JSON.parse(localStorage.getItem("proteins"));
        const id = proteins.length + 1;
        const protein = {name, flavor, price, quantity, id};

        if (name === "" || flavor === "" || price === "" || quantity === "") {
            alert("Please fill all the fields");
            return;
        }

        if (proteins === null) {
            proteins = [];
        }
        proteins.push(protein);
        localStorage.setItem("proteins", JSON.stringify(proteins));
        alert("Protein added successfully!");
        e.target.name.value = "";
        e.target.flavor.value = "";
        e.target.price.value = "";
        e.target.quantity.value = "";
        document.location.reload();
    }

    return (
        <form onSubmit={handleSubmit} className="addForm">
          <label htmlFor="name">Name:</label>
          <input type="text" id="name" name="name"/><br/><br/>
          
          <label htmlFor="flavor">Flavor:</label>
          <input type="text" id="flavor" name="flavor"/><br/><br/>
          
          <label htmlFor="price">Price:</label>
          <input type="number" id="price" name="price"/><br/><br/>
          
          <label htmlFor="quantity">Quantity:</label>
          <input type="number" id="quantity" name="quantity"/><br/><br/>
          
          <input type="submit" value="Add protein" className="addProtein"/>
        </form>
      );
}

export default AddProtein;