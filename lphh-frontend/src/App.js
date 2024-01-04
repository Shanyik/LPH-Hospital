
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css';
import Home from './components/Home/Home';
import Navbar from './components/NavBar/NavBar';
import DisplayPatients from './components/DisplayPatients/DisplayPatients';
import NotFound from './components/NotFound/NotFound';
import DisplayDoctors from "./components/DisplayDoctors/DisplayDoctors";
import { useEffect} from "react";
import DoctorMain from "./components/Doctor/DoctorMain";
import Prescriptions from "./components/Patient/Prescriptions/Prescriptions";
import Documents from "./components/Patient/Documents/Documents";
import Profile from "./components/Profile/Profile";
import ExaminationCreater from "./components/Doctor/ExaminationCreater";
import PatientHome from "./components/Patient/PatientHome/PatientHome";
import Login from "./components/Auth/Login";
import { CookiesProvider, useCookies } from "react-cookie";
import Registration from "./components/Auth/Registration";
import AdminHome from "./components/Admin/AdminHome";
import PrescriptionCreator from "./components/Doctor/PrescriptionCreator/PrescriptionCreator";

function App() {

  const [cookie, setCookie, removeCookie] = useCookies(['user-cookie']);

  useEffect(() => {
    console.log(cookie)
  },[cookie])

  return (
    <CookiesProvider>
      <Router>
        <div className="App">
          <Navbar cookie={cookie} removeCookie={removeCookie}></Navbar>
          <Routes>
            <Route exact path="*" element={<NotFound />}></Route> {/* 404 */}
            {
              cookie.role === "Doctor" ? [
                <>
                  <Route path="/patients" element={<DisplayPatients cookie={cookie}/>} />
                  <Route path="/examination" element={<ExaminationCreater userId={cookie.id}/>}></Route>
                  <Route path="/main" element={<DoctorMain />} />
                  <Route path="/profile" element={<Profile cookie={cookie}/>} />
                  <Route path="/prescriptioncreator" element={<PrescriptionCreator cookie={cookie}/>} />
                </>
              ] : cookie.role ==="Patient" ?  [
                <>
                  <Route path="/doctors" element={<DisplayDoctors />} /> 
                  <Route path="/patient/prescriptions" element={<Prescriptions cookie={cookie}/>} />
                  <Route path="/patient/documents" element={<Documents cookie={cookie}/>} />
                  <Route path="/patient/home" element={<PatientHome />} />
                  <Route path="/profile" element={<Profile cookie={cookie}/>} />
                </>
              ] :[
                <>
                 <Route path="/adminHome" element={<AdminHome/>} />
                  <Route path="/" element={<Home />} />
                  <Route path="/login" element={<Login setCookie={setCookie} cookie={cookie}/>} />
                  <Route path="/registration" element={<Registration cookie={cookie}/>} />
                </>
              ]
            }

          </Routes>

        </div>
      </Router>
    </CookiesProvider>
  );
}

export default App;
