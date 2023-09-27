import {  BrowserRouter as Router,  Routes,  Route} from "react-router-dom";
import './App.css';
import Home from './components/Home/Home';
import Navbar from './components/NavBar/NavBar';
import DisplayPatients from './components/DisplayPatients/DisplayPatients';
import NotFound from './components/NotFound/NotFound';


function App() {

  return (
    <Router>
      <div className="App">
        <Navbar/>
        <Routes> 
          <Route exact path="*" element={<NotFound/>}></Route> {/* 404 */}
          <Route path="/" element={<Home />} />  
          <Route path="/patients" element={<DisplayPatients/>} />        
        </Routes>
      </div>
    </Router>
  );
}

export default App;
