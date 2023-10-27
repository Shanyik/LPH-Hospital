import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import "./ModalDocument.css"

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: "30%",
    maxHeight: "70%",
    minHeight: "40%",
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    borderRadius: "30px",
    p: 4,
  };

const ModalDocument = (props) => {
  return (
    <div>
      <Modal
        open={props.show}
        onClose={props.close}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box className="modal-prescription" sx={style} alignItems="center">
          <Typography id="modal-modal-title" alaign="center">
            {props.data.type}
          </Typography>

          <Typography fontWeight={'bold'} className='modal-dropdown-title'>Result</Typography>
          <Typography className='modal-dropdown-description'>{props.data. result}</Typography>

          <Accordion>
            <AccordionSummary
              expandIcon={<ExpandMoreIcon />}
            >
              <Typography>Prescribing Doctor</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <Typography>
                <Typography fontWeight={'bold'} className='modal-dropdown-title'>Name</Typography>
                <Typography className='modal-dropdown-description'>{props.data.doctorName}</Typography>
              </Typography>
              <Typography>
                <Typography fontWeight={'bold'}  className='modal-dropdown-title'>Phone</Typography>
                <Typography className='modal-dropdown-description'>{props.data.doctorPhone}</Typography>
              </Typography>
              <Typography>
                <Typography fontWeight={'bold'} className='modal-dropdown-title'>E-mail</Typography>
                <Typography className='modal-dropdown-description'>{props.data.doctorEmail}</Typography>
              </Typography>
              <Typography>
                <Typography fontWeight={'bold'} className='modal-dropdown-title'>Ward</Typography>
                <Typography className='modal-dropdown-description'>{props.data.doctorWard}</Typography>
              </Typography>
            </AccordionDetails>
          </Accordion>

          <Button variant="contained" color="success" onClick={props.close} sx={{left: "43%", marginTop: "20px"}}>Close</Button>
        </Box>
      </Modal>
    </div>
  )
}

/*
{props.data.doctorName}
            {props.data.description}
            {props.data.productPacking}
            {props.data.productSubsitutable}
 */
export default ModalDocument