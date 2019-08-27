import React from 'react';
import logo from './logo.svg';
import './App.css';
import { HashRouter } from "react-router-dom";


function App() {
  return (
    <HashRouter basename="/sampleapp">
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
    </HashRouter>
  );
}

export default App;
