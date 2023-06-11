import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const App = () => {

const [items, setItems] = useState([]);

  
    useEffect(() => {
        const getItems = async () => {
            const { data } = await axios.get('/api/lakewoodscoop/scrape');
            setItems(data);
        }
        getItems();
    }, [])
return (
    <div>
    <h1 style={{ textAlign: "center" }}>The Lakewood Scoop</h1>
    <div style={{ display: "flex", justifyContent: "center", flexWrap: "wrap" }}>
      {items.map((item) => (
        <div
          key={item.url}
          style={{
            outline: "2px solid #ccc",
            padding: "20px",
            margin: "20px",
            maxWidth: "400px",
            textAlign: "center",
          }}
        >
          <h2>
            <a
              target="_blank"
              rel="noopener noreferrer"
              href={item.url}
              style={{ textDecoration: "none" }}
            >
              {item.title}
            </a>
          </h2>
          <p>{item.date}</p>
          <img
            src={item.image}
            style={{ width: "100%", height: "auto", borderRadius: "5px" }}
            alt="Ad Image"
          />
          <p>{item.text}</p>
          <div style={{ marginBottom: 0 }}>{item.comments}</div>
        </div>
      ))}
    </div>
  </div>
  

)}


export default App;