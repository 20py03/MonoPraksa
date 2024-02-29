import React from "react";
//import notFound from "../notFound.jpg";
import notFound2 from "../notFound2.jpg";

function NotFound() {
    return (
        <div className="not-found-container">
            
            <div className="not-found-text">
                <h1><b>404 - Not Found</b></h1>
                <img className="not-found-image" src={notFound2} alt="Not Found" />
            </div>
            {/*
            <div class="bottom-left">404 - Not Found</div>
            <div class="top-left">404 - Not Found</div>
            <div class="top-right">404 - Not Found</div>
            <div class="bottom-right">404 - Not Found</div>
            */}
        </div>
    );
}

export default NotFound;