import { useEffect, useState } from "react";
import DoctorMain from "../Doctor/DoctorMain";
import PatientMain from "../Patient/PatientHome/PatientHome";

const loginFetch = (data) => {
    return fetch('/Auth/Login',
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        }).then(res => res.json())
}

const getIdFetch = (id, role) => {
    return fetch(`/${role}/GetByIdentityId:${id}`).then(res => res.json())
}

const Login = ({ token, setToken, role, setRole, setUserID }) => {
    //const { role, setRole } = useContext(MyContext)
    const [email, setEmail] = useState(null)
    const [password, setPassword] = useState(null)
    const [result, setResult] = useState(null)

    /*useEffect(()=>{
        console.log(token)
    })*/

    const emailHandler = (input) => {
        setEmail(input.target.value)
        console.log(input.target.value)
    }

    const passwordHandler = (input) => {
        setPassword(input.target.value)
        console.log(input.target.value)
    }

    const loginHandler = () => {
        const data = {
            email: email,
            password: password
        }
        console.log(data)
        loginFetch(data).then(res => {
            setResult(res);
            if (res.token === undefined) {
                setResult(false)
            } else {
                setResult(true)
                const token = res.token;
                const decodedToken = JSON.parse(atob(token.split('.')[1]));
                const role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
                setRole(role)
                setToken(token)

                console.log(decodedToken)
            }
            console.log(res)
        })
    }

   useEffect(()=>{
        if((token !== "")){
            console.log(token)
            const decodedToken = JSON.parse(atob(token.split('.')[1]));
            const role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            const id = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            getIdFetch( id, role).then(
                data => {
                    setUserID(data.id);
                    localStorage.setItem('userId', data.id)
                    console.log(data)
                }
            )
        }
    },[token])

    return (

        role === null ? [
            <div id="loginContainer">

                <div>
                    Login
                </div>
                <div id="loginResult">
                    {result === false ? <>Wrong Email or password</> : <></>}
                </div>
                <div>
                    Username/Email:
                </div>
                <div>
                    <input type="text" onChange={(e) => { emailHandler(e) }}></input>
                </div>
                <div>
                    Password:
                </div>
                <div>
                    <input type="password" onChange={(e) => { passwordHandler(e) }}></input>
                </div>
                <div>
                    <button onClick={() => { loginHandler() }}>Login</button>
                </div>
            </div>
        ] : role === "Doctor" ? [
            <DoctorMain />
        ] : [
            <PatientMain />
        ]
    )
}

export default Login;