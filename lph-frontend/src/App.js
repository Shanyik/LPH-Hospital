import './App.css';

function App() {

  fetch('http://localhost:5227/Patient/GetAll')
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.log(error))

  return (
    <div className="App">
      <header className="App-header">
        
      </header>
    </div>
  );
}

export default App;
