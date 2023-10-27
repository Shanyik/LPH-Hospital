import { useEffect, useState } from "react";
import DoctorMain from "../Doctor/DoctorMain";
import PatientMain from "../Patient/PatientHome/PatientHome";
import { useNavigate } from "react-router-dom";

const loginFetch = (data) => {
  return fetch("/Auth/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  }).then((res) => res.json());
};

const getIdFetch = (id, role) => {
  return fetch(`/${role}/GetByIdentityId:${id}`).then((res) => res.json());
};

const Login = (props) => {
  const [email, setEmail] = useState(null);
  const [password, setPassword] = useState(null);
  const [result, setResult] = useState(null);

  const navigate = useNavigate();

  const emailHandler = (input) => {
    setEmail(input.target.value);
    console.log(input.target.value);
  };

  const passwordHandler = (input) => {
    setPassword(input.target.value);
    console.log(input.target.value);
  };

  const navigateToUrl = (role) => {
    if (role == "Doctor") {
      navigate("/main");
    } else {
      navigate("/patient/home");
    }
  };

  const loginHandler = () => {
    const data = {
      email: email,
      password: password,
    };
    console.log(data);
    loginFetch(data).then((res) => {
      if (res.token !== undefined) {
        const token = res.token;
        const decodedToken = JSON.parse(atob(token.split(".")[1]));
        const role =
          decodedToken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ];
        const id =
          decodedToken[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
          ];

        getIdFetch(id, role).then((data) => {
          props.setCookie("id", data.id, { maxAge: 3600 });
          props.setCookie("role", role, { maxAge: 3600 });
          navigateToUrl(role);
          window.location.reload(true);
        });

        console.log(decodedToken);
        console.log(res);
      } else {
        setResult(false);
      }
    });
  };

  const handleGoBack = () => {
    navigate("/")
  }

  return (
    <div className="container">
      <div className="form-heading">Login</div>
      <div id="loginResult" className="form-error">
        {result === false ? "Wrong Email or password" : ""}
      </div>
      <div className="form-group">
        <label className="form-label">Username/Email:</label>
        <input
          type="text"
          onChange={(e) => {
            emailHandler(e);
          }}
          className="form-input"
        />
      </div>
      <div className="form-group">
        <label className="form-label">Password:</label>
        <input
          type="password"
          onChange={(e) => {
            passwordHandler(e);
          }}
          className="form-input"
        />
      </div>
      <button onClick={() => (handleGoBack())} className="form-goBack">Go Back</button>
      <button onClick={() => loginHandler()} className="form-submit">Login</button>
    </div>
  );
};

export default Login;
