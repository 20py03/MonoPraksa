import './App.css';
import Home from './components/Home';
import Footer from './components/Footer';
import Navbar from './components/Navbar';
import AddProtein from './components/AddProtein';
import ProteinList from './components/ProteinList';
import NotFound from './components/NotFound';
import { useState } from 'react';
//import AddProteinClass from './AddProteinClass';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {
  const [proteins, setProteins] = useState([]);

  return (
    <>
    <BrowserRouter>
      <Navbar/>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/addProtein" element={<AddProtein setProteins={setProteins} />} />
        <Route path="/proteinList" element={<ProteinList setProteins={setProteins} proteins={proteins} />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
      <Footer/>
    </BrowserRouter>
    </>
  );
}

export default App;
