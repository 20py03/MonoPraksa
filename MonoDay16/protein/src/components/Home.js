import React from 'react';
import { Grid, Box } from '@mui/material'
import pic1 from '../pic1.jpg';
import pic2 from '../pic2.png';
import pic3 from '../pic3.jpg';

function Home() {
    return(
        <center>  
            <div className="home">
                <h1>Welcome to Protein Shop</h1>
                <p>
                    Discover the best selection of high-quality proteins for your fitness goals.
                </p>
                <p>
                    Whether you're looking to build muscle, recover after a workout, or maintain a healthy lifestyle, we have the perfect proteins for you.
                </p>
                <p>
                    Explore our wide range of flavors, brands, and categories to find the protein that fits your needs.
                </p>
                <button className='formButton'><a href="/proteinList">See available flavours</a></button>
            </div>
            <Grid container spacing={1}>
                <div className='second_section'>
                
                    <Grid item xs={4}> 
                        <Box>
                            <img src={pic1} alt="Protein shake" className="home_imgs" style={{width:"50%", height: "50%"}}/>
                        </Box> 
                    </Grid>

                    <Grid item xs={4}> 
                        <Box> 
                            <img src={pic2} alt="Whey protein" className="home_imgs" style={{width:"50%",height:"50%"}}/>
                        </Box> 
                    </Grid> 

                    <Grid item xs={4}> 
                        <Box> 
                            <img src={pic3} alt="Proteins" className="home_imgs" style={{width:"50%",height:"50%"}}/>
                        </Box> 
                    </Grid>
                    
                </div>
            </Grid>
        </center> 
    );
}

export default Home;