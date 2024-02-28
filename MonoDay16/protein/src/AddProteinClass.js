import React from 'react';

class AddProteinClass extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            flavor: '',
            price: '',
            quantity: ''
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        const id = parseInt(localStorage.getItem("proteins")?.length || 0) + 1;
        const protein = { name: this.state.name, flavor: this.state.flavor, price: this.state.price, quantity: this.state.quantity, id };

        if (this.state.name === "" || this.state.flavor === "" || this.state.price === "" || this.state.quantity === "") {
            alert("Please fill all the fields");
            return;
        }

        if (this.state.price < 0 || this.state.quantity < 0) {
            alert("Price and Quantity should be a positive number");
            return;
        }

        let proteins = JSON.parse(localStorage.getItem("proteins")) || [];

        proteins.push(protein);
        localStorage.setItem("proteins", JSON.stringify(proteins));
        this.props.setProteins(proteins);
        alert("Protein added successfully!");
        this.setState({ name: '', flavor: '', price: '', quantity: '' });
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }
    
    render() {
        const { name, flavor, price, quantity } = this.state;
        return (
            <form onSubmit={this.handleSubmit} className="addForm">
                <label htmlFor="name">Name:</label>
                <input type="text" id="name" name="name" value={name} onChange={this.handleChange} /><br /><br />

                <label htmlFor="flavor">Flavor:</label>
                <input type="text" id="flavor" name="flavor" value={flavor} onChange={this.handleChange} /><br /><br />

                <label htmlFor="price">Price(€):</label>
                <input type="number" id="price" name="price" value={price} onChange={this.handleChange} /><br /><br />

                <label htmlFor="quantity">Quantity:</label>
                <input type="number" id="quantity" name="quantity" value={quantity} onChange={this.handleChange} /><br /><br />

                <input type="submit" value="Add protein" className="addProtein" />
            </form>
        );
    }
}

export default AddProteinClass;