import http from "../http-common";

const getAll = (filtering, sorting, paging) => {
    let url = "/Protein";
    if (filtering) {
        url += `?flavor=${filtering.flavor}&minPrice=${filtering.minPrice}&maxPrice=${filtering.maxPrice}&minWeight=${filtering.minWeight}&maxWeight=${filtering.maxWeight}`;
    }
    if (sorting) {
        url += `&sortBy=${sorting.sortBy}&sortOrder=${sorting.sortOrder}`;
    }
    if (paging) {
        url += `&pageNumber=${paging.pageNumber}&pageSize=${paging.pageSize}`;
    }
    return http.get(url)
};

const create = protein => {
    return http.post("/Protein", protein);
};

const update = async (editId, data) => {
    try {
        const response = await http.put(`https://localhost:44371//Api/Protein/${editId}`, data);
        return response.data; 
    } catch (error) {
        throw error; 
    }
};

const remove = id => {
    return http.delete(`/Protein/${id}`);
};

const getCategory = () => { 
    return http.get("/Protein/Categories");
};

const ProteinService = {
    getAll,
    create,
    update,
    remove,
    getCategory
};

export default ProteinService;
