import React,{useEffect, useState}from 'react'
import {useParams, useNavigate} from 'react-router-dom'
import { MDBCol, MDBContainer, MDBRow, MDBCard, MDBCardText, MDBCardBody, MDBCardImage, MDBTypography, MDBIcon, MDBBtn} from 'mdb-react-ui-kit';

const ViewProject=()=>{
const[project, setProject]= useState(
  {
    "id": 0,
    "name": "",
    "startDate": "",
    "endDate": "",
    "technology": "",
    "nfRequirement": "",
    "budget": 0,
    "client_Projects": null,
    "teams":[]
}
);
const[clients, setClients]= useState([]);



const user =  JSON.parse(localStorage.getItem('user-info'));
const navigate = useNavigate();
const {id}= useParams();

  const loadData= async()=>{
        await fetch(`http://localhost:44327/api/Project/details/${id}`,{
           
                headers:{
                    Authorization : `Bearer ${user.token}`
                }
            
        })
        .then(res=>res.json())
        .then(res=>{
            
            setProject(res);
            
        })
  }

  const loadClients=async()=>{
    await fetch(`http://localhost:44327/api/Client/clients-working-project/${id}`,{        
                headers:{
                    Authorization : `Bearer ${user.token}`
                }
            
        })
        .then(res=>res.json())
        .then(res=>{
            setClients(res);
        })
  }

  useEffect(()=>{
   loadData();
   loadClients();
   
  },[])
  
  return (
    <section className="vh-100" >
      <MDBContainer className="py-5 h-100">
        <MDBRow className="justify-content-center align-items-center h-100">
          <MDBCol lg="6" className="mb-4 mb-lg-0">
            <MDBCard className="mb-3" style={{ borderRadius: '.5rem' }}>
              <MDBRow className="g-0">
                <MDBCol md="4" className="gradient-custom text-center text-white"
                  style={{ borderTopLeftRadius: '.5rem', borderBottomLeftRadius: '.5rem' }}>
                  {/* <MDBCardImage src={adminInfo.profileImageSrc}
                    alt="Avatar" className="my-5" style={{ width: '80px' }} fluid /> */}
                  <MDBTypography tag="h5">XXX</MDBTypography>
                  <MDBIcon far icon="edit mb-5" />
                </MDBCol>
                <MDBCol md="8">
                  <MDBCardBody className="p-4">
                  <MDBTypography tag="h5">{project.name}</MDBTypography>
                  <hr className="mt-0 mb-4"/>
                    <MDBRow className="pt-1">
                      <MDBCol size="6" className="mb-3">
                        <MDBTypography tag="h6">Start Date</MDBTypography>
                        <MDBCardText className="text-muted">{project.startDate.split("T")[0]}</MDBCardText>
                      </MDBCol>
                      <MDBCol size="6" className="mb-3">
                        <MDBTypography tag="h6">End Date</MDBTypography>
                        <MDBCardText className="text-muted">{project.endDate.split("T")[0]}</MDBCardText>
                      </MDBCol>
                    </MDBRow>
                    
                    <hr className="mt-0 mb-4"/>
                    <MDBRow className="pt-1">
                      <MDBCol size="6" className="mb-3">
                        <MDBTypography tag="h6">Budget</MDBTypography>
                        <MDBCardText className="text-muted">{project.budget}Cr</MDBCardText>
                      </MDBCol>
                      <MDBCol size="6" className="mb-3">
                        <MDBTypography tag="h6">Technology</MDBTypography>
                        <MDBCardText className="text-muted">{project.technology}</MDBCardText>
                      </MDBCol>
                    </MDBRow>
                    <hr className="mt-0 mb-4"/>
                    <MDBRow className="pt-1">
                      <MDBCol size="6" className="mb-3">
                        <MDBTypography tag="h6">Functional Requirement</MDBTypography>
                        <MDBCardText className="text-muted">{project.fRequirement}</MDBCardText>
                      </MDBCol>
                      <MDBCol size="6" className="mb-3">
                        <MDBTypography tag="h6">Non Functional Requirement</MDBTypography>
                        <MDBCardText className="text-muted">{project.nfRequirement}</MDBCardText>
                      </MDBCol>
                    </MDBRow>
                    <hr className="mt-0 mb-4"/>

                    <MDBRow className="pt-1">
                          <MDBCol size="6" className="mb-3">
                            <MDBTypography tag="h6">Clients</MDBTypography>                           
                          </MDBCol>
                          
                    </MDBRow>
                    {
                          clients !== null && clients.length!=0 ?(
                            clients.map((e,i)=>
                            <ul key={i} className="list-group list-group-light">
                                <li className="list-group-item d-flex justify-content-between align-items-center">
                                  <div className="d-flex align-items-center">

                                    <div className="ms-3">
                                      <p className="fw-bold mb-1">{e.name}</p>
                                    </div>
                                  </div>
                                  {/* <button className="btn  btn-rounded btn-sm mr-2" onClick={()=>{navigate(`/admin/client/project/${e.id}`)}}>View</button> */}
                                  {/* <button className="btn  btn-rounded text-danger btn-sm" onClick={()=>{deallocateProject(e)}}>Deallocate</button> */}
                                </li>
                              </ul>
                          )
                          ):(
                            <p style={{color:'red'}}>No clients availabe</p>
                          )
                        }
                    <hr className="mt-0 mb-4"/>

                    <MDBRow className="pt-1">
                          <MDBCol size="6" className="mb-3">
                            <MDBTypography tag="h6">Teams working on</MDBTypography>                           
                          </MDBCol>
                          
                    </MDBRow>
                    {
                          project.teams !== null && project.teams.length!=0 ?(
                            project.teams.map((e,i)=>
                            <ul key={i} className="list-group list-group-light">
                                <li className="list-group-item d-flex justify-content-between align-items-center">
                                  <div className="d-flex align-items-center">

                                    <div className="ms-3">
                                      <p className="fw-bold mb-1">{e.name}</p>
                                    </div>
                                  </div>
                                  {/* <button className="btn  btn-rounded btn-sm mr-2" onClick={()=>{navigate(`/admin/client/project/${e.id}`)}}>View</button> */}
                                  {/* <button className="btn  btn-rounded text-danger btn-sm" onClick={()=>{deallocateProject(e)}}>Deallocate</button> */}
                                </li>
                              </ul>
                          )
                          ):(
                            <p style={{color:'red'}}>No Teams assigned</p>
                          )
                        }
                    <MDBRow className="pt-1">
                          <MDBCol size="6" className="mb-3">
                            <MDBTypography tag="h6">Status</MDBTypography> 
                            {project.status === 'Active' &&
                              <MDBCardText className="text-success ">{project.status}</MDBCardText>
                            }
                            {project.status === 'Cancelled' &&
                              <MDBCardText className="text-danger ">{project.status}</MDBCardText>
                            }
                            {project.status === 'On hold' &&
                              <MDBCardText className="text-warning ">{project.status}</MDBCardText>

                            }
                            {project.status === 'Completed' &&
                              <MDBCardText className="text-success ">{project.status}</MDBCardText>
                            }
                            
                          </MDBCol>
                          
                          
                    </MDBRow>
                    <button className="btn btn-secondary mt-2"  onClick={()=>{navigate(`/admin/projects`)}} >Back</button>
                  </MDBCardBody>
                </MDBCol>
              </MDBRow>
            </MDBCard>
          </MDBCol>
        </MDBRow>  
      </MDBContainer>
    </section>
  );
}

export default ViewProject;