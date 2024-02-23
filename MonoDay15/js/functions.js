function addProtein(event) {
    event.preventDefault();

    let name = document.getElementById("name").value;
    let flavor = document.getElementById("flavor").value;
    let price = document.getElementById("price").value;
    let quantity = document.getElementById("quantity").value;

    if (name === "" || flavor === "" || price === "" || quantity === "") {
        alert("Enter all fields.");
        return;
    }

    let proteins = JSON.parse(localStorage.getItem("proteins")) || [];

    let protein = {
        id: proteins.length + 1,
        name: name,
        flavor: flavor,
        price: price,
        quantity: quantity
    };

    proteins.push(protein);

    localStorage.setItem("proteins", JSON.stringify(proteins));

    alert("Protein succesfully added!");

    window.location.href = "index.html";
    allProteins();
}

function allProteins() {
    let proteins = JSON.parse(localStorage.getItem("proteins"));

    if (proteins) {
        let tabela = document.getElementById("proteinsTable");
        tabela.innerHTML = ""; 

        let table = "<table>";
        table += "<tr><th>Name</th><th>Flavor</th><th>Price(€)</th><th>Quantity</th><th>Action</th></tr>";

        proteins.forEach(function(protein) {
            table += "<tr>";
            table += "<td>" + protein.name + "</td>";
            table += "<td>" + protein.flavor + "</td>";
            table += "<td>" + protein.price + "</td>";
            table += "<td>" + protein.quantity + "</td>";
            table += `<td><button onclick='editProtein(${protein.id})'>Edit</button>`;
            table += `<button onclick='deleteProtein(${protein.id})'>Delete</button></td>`;
            table += "</tr>";
        });

        table += "</table>";
        tabela.innerHTML = table;
    } else {
        alert("No proteins available.");
    }
}

function deleteProtein(id) {
    let deleteConfirmed = confirm("Are you sure you want to delete this protein?");

    if (deleteConfirmed) {
        let proteins = JSON.parse(localStorage.getItem("proteins"));

        let index = proteins.findIndex(function(protein) {
            return protein.id === id;
        });

        proteins.splice(index, 1);

        localStorage.setItem("proteins", JSON.stringify(proteins));

        allProteins();
    } else {
        console.log("Delete canceled.");
    }
}

function editProtein(id) {
    let proteins = JSON.parse(localStorage.getItem("proteins"));

    let protein = proteins.find(protein => protein.id === id);

    if (protein) {
        window.document.body.innerHTML += `<div id="editProtein">
        <div class="welcome_header"><h4>Edit your protein</h4></div>
        <form id="proteinForm" onsubmit="return false" class="addForm">
            <label for="name">Name:</label>
            <input type="text" id="name" name="name" value="${protein.name}"><br><br>
            
            <label for="flavor">Flavor:</label>
            <input type="text" id="flavor" name="flavor" value="${protein.flavor}"><br><br>
    
            <label for="price">Price:</label>
            <input type="number" id="price" name="price" value="${protein.price}"><br><br>
    
            <label for="quantity">Quantity:</label>
            <input type="number" id="quantity" name="quantity" value="${protein.quantity}"><br><br>
    
            <input type="submit" value="Edit protein" class="addProtein" onClick="editProteinData(${id})">
        </form>
    </div>`
        window.document.getElementById("action_header").style.display ="none";
        window.document.getElementById("ordered_list").style.display ="none";
        window.document.getElementById("proteinsTable").style.display ="none";

    } else {
        alert("Protein not found.");
    }
}

function editProteinData(id)
{
    let name = document.getElementById("name").value;
    let flavor = document.getElementById("flavor").value;
    let price = document.getElementById("price").value;
    let quantity = document.getElementById("quantity").value;

    if (name === "" || flavor === "" || price === "" || quantity === "") {
        alert("Enter all fields.");
        return;
    }

    let proteins = JSON.parse(localStorage.getItem("proteins"));
    proteins = proteins.filter(protein => protein.id != id);

    let protein = {
        id: proteins.length + 1,
        name: name,
        flavor: flavor,
        price: price,
        quantity: quantity
    };

    proteins.push(protein);

    localStorage.setItem("proteins", JSON.stringify(proteins));

    alert("Protein succesfully edited!");
    window.document.getElementById("action_header").style.display ="block";
    window.document.getElementById("proteinsTable").style.display ="flex";
    window.document.getElementById("ordered_list").style.display ="block";
    allProteins();
    window.document.getElementById("editProtein").remove();

}