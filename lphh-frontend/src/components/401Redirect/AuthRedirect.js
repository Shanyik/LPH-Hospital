

const logoutFetch = () => {
    return fetch('api/Auth/Logout', {
        method: 'GET'
    }).then(response => response.json());
};

const fetchOrigin = (originUrl, originOptions) => {
    return fetch(originUrl, originOptions).then(res => res.json())
}

//https://blog.theashishmaurya.me/handling-jwt-access-and-refresh-token-using-axios-in-react-app
//https://www.bezkoder.com/axios-interceptors-refresh-token/
const refreshAuthToken = async (originUrl, originOptions) => {
    try {
        const res = await fetch('api/Auth/RefreshToken');
        if (res.status === 200) {
            return fetchOrigin(originUrl, originOptions)
           
        } else {
            console.log("nem nem soha");
            //itt lesz a logout
        }
        
    } catch (error) {
        console.log(error);
    }
};

async function handle401(response, a,b) {
    if (response.status === 401) {
        const res = refreshAuthToken(a,b);
        console.log(res)
        return res
    }else{
       
        return response.json();
    }
}

const fetchWithInterceptor =  (url, options) => {
    return fetch(url, options)
    .then(response => {return handle401(response, url, options)})
    
};



export { fetchWithInterceptor, refreshAuthToken };

