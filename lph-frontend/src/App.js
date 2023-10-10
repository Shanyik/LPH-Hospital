import {  BrowserRouter as Router,  Routes,  Route} from "react-router-dom";
import './App.css';
import Home from './components/Home/Home';
import Navbar from './components/NavBar/NavBar';
import DisplayPatients from './components/DisplayPatients/DisplayPatients';
import NotFound from './components/NotFound/NotFound';
import DisplayDoctors from "./components/DisplayDoctors/DisplayDoctors";
import { useState } from "react";
import DoctorMain from "./components/Doctor/DoctorMain";


function App() {

  const [user, setUser] = useState(null)
  console.log(user)
  return (
    <Router>
      <div className="App">
      <Navbar user={user} setUser={setUser}></Navbar>
        <Routes> 
          <Route exact path="*" element={<NotFound/>}></Route> {/* 404 */}
          <Route path="/" element={<Home setUser={setUser}/>} />  
          <Route path="/patients" element={<DisplayPatients user={user} setUser= {setUser}/>} />   
          <Route path="/doctors" element={<DisplayDoctors/>} />
          <Route path="/main" element={<DoctorMain user={user} setUser= {setUser}/>} />    
        </Routes>
        
      </div>
    </Router>
  );
}

export default App;
