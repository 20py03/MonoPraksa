import React from 'react';
import { Link } from 'react-router-dom';

function Navbar() {
  return(
    <header>
      <nav>
          <Link to="/">HOME</Link>
          <Link to="/addProtein">ADD PROTEIN</Link>
          <Link to="/proteinList">PROTEIN LIST</Link>
      </nav> 
    </header>
);
}

export default Navbar;