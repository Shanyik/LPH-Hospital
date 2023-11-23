import { useEffect, useState } from "react";
import "./PrescriptionCreator.css"
import Typography from '@mui/material/Typography';
import { useNavigate } from 'react-router-dom';
import { Grid } from "@mui/material";
import Button from '@mui/material/Button';

const PrescriptionCreator = (props) => {

    const [doctor, setDoctor] = useState(null)
    const [products, setProducts] = useState(null)
    const [medicalNumber, setMedicalNumber] = useState("")
    const [patient, setPatient] = useState(null)

    const [filteredProducts, setFilteredProducts] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [description, setDescription] = useState("")
    const [selectedProduct, setSelectedProduct] = useState(null);

    const navigate = useNavigate()

    const getDoctorById = (userId) => {
        return fetch(`api/Doctor/GetById:${userId}`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }).then(res => res.json())
    }

    const getAllProducts = () => {
        return fetch('api/Product/GetAll', {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }).then(res => res.json())
    }

    
    const getPatientByMedicalNumber = (medicalNumber) => {
        return fetch(`api/Patient/GetByMedicalNumber:${medicalNumber}`, {
            method: 'GET',
            headers: {
            'Authorization': 'Bearer ' + props.cookie["token"]
            }
        }).then(res => res.json())
    }

    useEffect(() => {
        getDoctorById(props.cookie["id"])
            .then(data => {
                setDoctor(data)
                console.log(data.lastName)
            })

        getAllProducts()
            .then(data => {
                setProducts(data)
                console.log(data)
            })
    }, [props.cookie["id"]])

    const handleMedicalNumberChange = (event) => {
        setMedicalNumber(event.target.value);
        const isNumeric = /^\d{3}-\d{3}-\d{3}$/.test(event.target.value);

        if (isNumeric) {
            getPatientByMedicalNumber(event.target.value).then(data => {
                if (data.status === 404) {
                    console.log(data)
                } else {
                    setPatient(data)
                    console.log(patient)
                }
            })
        }
        else {
            setPatient(null)
        } 
    };

    const handleSearch = (event) => {
        const searchTerm = event.target.value.toLowerCase();
        setSearchTerm(searchTerm);
    
        const filtered = products.filter((product) =>
          product.name.toLowerCase().includes(searchTerm)
        );
    
        setFilteredProducts(filtered);
      };

    const handleSelectProduct = (event) => {
        const selectedProductName = event.target.value;
        const selectedProduct = products.find(
          (product) => product.name === selectedProductName
        );
    
        setSelectedProduct(selectedProduct);
    };

    const addPrescription = (data) => {
        return fetch('api/Prescription/Add', {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': 'Bearer ' + props.cookie["token"]
            },
            body: JSON.stringify(data),
        }).then((res) => console.log("success"));
    }

    const onSubmit = (e) => {
        e.preventDefault();
        console.log(selectedProduct)
        if (patient !== null && selectedProduct !== null) {
            const data = {
                productid: selectedProduct.id,
                doctorid: doctor.id,
                patientId: patient.id,
                description: description,
                createdAt: new Date()
            }
            console.log(data)
            addPrescription(data)
            navigate("/patients");
        } 
    };

    return (
        doctor != null ? (
            <div id="examCreaterContainer">
                <form className="ExamForm" onSubmit={onSubmit}>

                <div className="form-group">
                    <label className="form-label">Medical Number:</label>
                    <input
                    type="text"
                    name="medicalNumber"
                    value={medicalNumber}
                    onChange={(e) => handleMedicalNumberChange(e)}
                    className="form-input"
                    />
                </div>

                {
                    patient !== null ? (
                        <>
                            <div> {/* margin */}
                                <Grid container alignItems="center">
                                    <Grid item xs={6} >
                                        <Typography htmlFor="name">Firstname:</Typography>
                                    </Grid>
                                    <Grid item xs={6} >
                                        <Typography htmlFor="name">Lastname:</Typography>
                                    </Grid>
                                </Grid>
                                <Grid container alignItems="center">
                                    <Grid item xs={6} >
                                        <h2>{patient.firstName}</h2>
                                    </Grid>
                                    <Grid item xs={6} >
                                        <h2>{patient.lastName}</h2>
                                    </Grid>
                                </Grid>
                            </div>

                            <div>
                                <label htmlFor="productSearch">Search for a product:</label>
                                <input
                                    type="text"
                                    id="productSearch"
                                    value={searchTerm}
                                    onChange={handleSearch}
                                />
                                <div>
                                    <label htmlFor="productDropdown">Select a product:</label>
                                    <select
                                        id="productDropdown"
                                        value={selectedProduct ? selectedProduct.name : ''}
                                        onChange={handleSelectProduct}
                                    >
                                        <option value="" disabled>
                                        Select a product
                                        </option>
                                        {
                                            filteredProducts.length === 0 ? (
                                                products.map((product, index) => (
                                                    <option key={index} value={product.name}>
                                                        {product.name}
                                                    </option>
                                                ))
                                            ) : (
                                                filteredProducts.map((product, index) => (
                                                    <option key={index} value={product.name}>
                                                        {product.name}
                                                    </option>
                                                ))
                                            )}
                                    </select>
                                </div>
                            </div>

                            <div id="descriptionContainer">
                                <div className="control">
                                    <h4>Description</h4>
                                    <textarea
                                        className="descriptionTextArea"
                                        onChange={(e) => setDescription(e.target.value)}
                                        name="medicalNumber"
                                        id="medicalNumber"
                                    />
                                </div>
                            </div>
                            
                            <Button variant="contained" color="success" type="submit">
                                Submit
                            </Button>
                        </>
                     ) : (
                        <></>
                     )
                }  
                </form>
            </div>
         ) : (
            null
        )
    )
}

export default PrescriptionCreator