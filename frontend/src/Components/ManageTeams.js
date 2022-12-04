import React ,{useEffect, useState} from 'react'
import { useNavigate } from 'react-router';
import { toast, ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

const ManageTeams = () => {
    const[teams, setTeams]= useState([]);
    const[employees, setEmployees]= useState([]);
    const[showEmp, setShowemp]= useState(true);
    const[isempdeleted, setIsempdeleted]= useState(false);
    const navigate= useNavigate();
    const loadTeams=async()=>{
        await fetch(`http://localhost:44327/api/Team/all-teams`,{
            //   headers:{
            //       "Authorization" : `Bearer ${user.token}`
            //   }
           })
           .then(res=> res.json())
           .then(res=>{
              setTeams(res);
              
           })
    }
    const loadEmployees=async()=>{
       await  fetch(`http://localhost:44327/api/Employee/employees`,{
        //   headers:{
        //       "Authorization" : `Bearer ${user.token}`
        //   }
       })
       .then(res=> res.json())
       .then(res=>{
          setEmployees(res);
          
       })
    }
    useEffect(()=>{
         loadTeams();
         loadEmployees();
    },[])
    
    const deleteEmployee=(employeeId,employeeName)=>{
        if(!window.confirm("Do you really want to delete?")){
          return;
        }
        
            fetch(`http://localhost:44327/api/Employee/delete-employee/${employeeId}`,{
              method:'DELETE',
            //   headers:{
            //     Authorization:`Bearer ${user.token}`
            //   }
            })
            .then(r=>r.json())
            .then(r=>{
              toast.success(employeeName+" deleted successfully", {
                position: toast.POSITION.TOP_RIGHT
              });
              setIsempdeleted(true);
              setTimeout(()=>{
                loadEmployees();
              },2000)
            })
        
      }


    return (
        <div className='container mt-2'>
            <button className='btn btn-secondary' onClick={()=>{navigate("/admin/create-team")}}>Create Team</button>
            <button className='btn btn-secondary ml-2' onClick={()=>{navigate("/admin/create-employee")}}>Create Employee</button>
            <button className='btn btn-secondary ml-2' onClick={()=>{setShowemp(true)}}>All Employees</button>
            <button className='btn btn-secondary ml-2' onClick={()=>{setShowemp(false)}}>All Teams</button>
            {showEmp== false? 
            <div id="accordion" className='mt-2'>
            {teams.map((e, i)=>
            <div class="card"  key={i}>
            <div class="card-header" id="headingOne">
                <h5 class="mb-0">
                    <button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        {e.name} 
                    </button>
                </h5>
            </div>

            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                <div class="card-body">
                    {/* <button className='btn btn-sm btn-info'>Edit</button>
                    <button className='btn btn-sm ml-2 btn-danger'>Delete</button> */}
                     {e.projectId == null ?
                      <p style={{color:'red'}}>Not working on any Project</p>
                      : <p >Working on Project: <p className='font-weight-bold'>{e.project.name}</p></p>
                     }
                     
                    <table class="table ">
                        <tbody> 
                            <tr>
                                <th >Name</th>
                                <th>Email</th>
                                <th>Phone</th>
                                <th>Designation</th>
                                
                            </tr>
                            {e.employees.map((ele,index)=>
                            <tr key={index}>
                            <td>{ele.name}</td>
                            <td>{ele.email}</td>
                            <td>{ele.phone}</td>
                            <td>{ele.designation}</td>
                            
                            </tr>
                            )}
                            
                        </tbody>
                    </table>
                </div>
            </div>
        </div>



            )}

        </div> :
        <table class="table mt-2">
        <tbody> 
            <tr>
                <th >Name</th>
                <th>Email</th>
                <th>Phone</th>
                <th>Designation</th>
                <th>Action</th>
            </tr>
            {employees.map((ele,index)=>
            <tr key={index}>
            <td>{ele.name}</td>
            <td>{ele.email}</td>
            <td>{ele.phone}</td>
            <td>{ele.designation}</td>
            <td>
                <button className='btn btn-sm' onClick={()=>{navigate(`/admin/edit-employee/${ele.id}`)}}>Edit</button>
            </td>
            <td>
                <button className='btn btn-sm' onClick={()=>deleteEmployee(ele.id, ele.name)}>delete</button>
            </td>
            </tr>
            )}
            
        </tbody>
    </table>
        }
        
        <ToastContainer/>
        </div>
    )
}

export default ManageTeams;