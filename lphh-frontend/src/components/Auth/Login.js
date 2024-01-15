import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./login.css";


const loginFetch = (data) => {
  return fetch(`api/Auth/Login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  }).then((res) => res.json());
};

const getIdFetch = (id, role) => {
  return fetch(`api/${role}/GetByIdentityId:${id}`, {
    method: 'GET',
  }).then((res) => res.json());
};

const Login = (props) => {
  const [email, setEmail] = useState(null);
  const [password, setPassword] = useState(null);
  const [result, setResult] = useState(null);

  const [loading, setLoading] = useState(null);

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
    if (role === "Doctor") {
      navigate("/main");
    } else if (role === "Patient") {
      navigate("/patient/home");
    } else {
      navigate("/adminHome");
    }
  };

  const loginHandler = () => {
    const data = {
      email: email,
      password: password,
    };
    console.log(data);
    if (data.email === null || data.password === null) {
      setResult("noData")
    } else {
      loginFetch(data).then((res) => {

        if (res.token !== undefined) {
          setLoading(true);
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
          getIdFetch(id, role, token).then((data) => {
            setTimeout(() => {
              localStorage.setItem('kulcs', 'érték');
              localStorage.setItem('id', data.id);
              localStorage.setItem('role', role);
              props.setRefresh(role);
              props.setId(data.id);
              navigateToUrl(role);
            }, 3000);

          });

          console.log(decodedToken);
          console.log(res);
        } else {
          setLoading(null);
          setResult(false);
        }
      });
    }

  };

  const handleGoBack = () => {
    navigate("/")
  }

  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      loginHandler();
    }
  }

  return (
    <div className="container">
      {
        loading === null ? (
          <>
            <div className="form-heading">Login</div>
            <div id="loginResult" className="form-error">
              {result === false ? "Wrong Email or password" :
                result === "noData" ? "Email/Username and password required!" : ""}
            </div>
            <div className="form-group">
              <label className="form-label">Email:</label>
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
                onKeyDown={(e) => handleKeyPress(e)}
                className="form-input"
              />
            </div>
            <button onClick={() => (handleGoBack())} className="form-goBack">Go Back</button>
            <button onClick={() => loginHandler()} className="form-submit">Login</button>
          </>
        ) : (
          <>
            <span className="loader">

            </span>
          </>
        )
      }

    </div>
  );
};

export default Login;
