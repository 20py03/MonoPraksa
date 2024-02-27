import './App.css';
import Footer from './Footer';
import Navbar from './Navbar';
import AddProtein from './AddProtein';
import ProteinList from './ProteinList';
import protein from './protein.jpg';

function App() {
  return (
    <>
    <Navbar/>
    <div id="action_header" class="action_header"><h3>Add your favourite</h3></div>
    <div className='addProteinContainer'>
      <div><AddProtein/></div>
      <div><img src={protein} alt="protein" className="proteinImage"/></div>
    </div>
    <hr></hr>
    <div id="action_header" class="action_header"><h3>List of available proteins</h3></div>
    <ProteinList/>
    <Footer/>
    </>
  );
}

export default App;
