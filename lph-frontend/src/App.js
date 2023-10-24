
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css';
import Home from './components/Home/Home';
import Navbar from './components/NavBar/NavBar';
import DisplayPatients from './components/DisplayPatients/DisplayPatients';
import NotFound from './components/NotFound/NotFound';
import DisplayDoctors from "./components/DisplayDoctors/DisplayDoctors";
import { useEffect, useState } from "react";
import DoctorMain from "./components/Doctor/DoctorMain";
import Prescriptions from "./components/Patient/Prescriptions/Prescriptions";
import Documents from "./components/Patient/Documents/Documents";
import Profile from "./components/Profile/Profile";
import ExaminationCreater from "./components/Doctor/ExaminationCreater";
import PatientHome from "./components/Patient/PatientHome/PatientHome";
import Login from "./components/Auth/Login";


function App() {

  const [user, setUser] = useState(null)
  const [userId, setUserID] = useState(1)
  const [token, setToken] = useState(null)
  const [role, setRole] = useState(null)
  //const [cookies, setCookie, removeCookie] = useCookies(['cookie-name']);

  useEffect(() => {
   if (token !== null ) {

      localStorage.setItem('token', token)
      console.log(localStorage)

    } else{
      console.log(localStorage.getItem('token'))
      let refreshToken = localStorage.getItem('token');
      setToken(refreshToken);
      console.log(refreshToken)
      let decoded = JSON.parse(atob(refreshToken.split(".")[1]));
      let exp = decoded.exp;
      const currentTimeInSeconds = Math.floor(Date.now() / 1000);
      if (exp < currentTimeInSeconds) {
        setRole(null)
      } else {
        console.log(decoded)
        const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        console.log(decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"])
        setRole(role)
      }


    }

  }, [token])

  useEffect(()=>{
    console.log("asd")
    setUserID(localStorage.getItem('userId'))
  },[window.location.pathname])

  return (
    <Router>
      <div className="App">
        <Navbar user={user} setUser={setUser}></Navbar>
        <Routes>
          <Route exact path="*" element={<NotFound />}></Route> {/* 404 */}
          <Route path="/" element={<Home setUser={setUser} />} />
          <Route path="/login" element={<Login token={token} setToken={setToken} role={role} setRole={setRole} setUserID={setUserID}/>} />
          {
            role === "Doctor" ? [
              <>
                <Route path="/patients" element={<DisplayPatients user={user} setUser={setUser} />} />
                <Route path="/examination" element={<ExaminationCreater userId={userId} />}></Route>
                <Route path="/main" element={<DoctorMain user={user} setUser={setUser} />} />
              </>
            ] : [
              <>
                <Route path="/doctors" element={<DisplayDoctors />} />
                <Route path="/patient/prescriptions" element={<Prescriptions userId={userId} />} />
                <Route path="/patient/documents" element={<Documents />} />
                <Route path="/patient/home" element={<PatientHome />} />
              </>
            ]
          }
          <Route path="/profile" element={<Profile role={role} userId={userId}/>} />
        </Routes>

      </div>
    </Router>
  );
}

export default App;
