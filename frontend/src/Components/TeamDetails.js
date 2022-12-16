import React, {useState, useEffect} from 'react'
import {useParams}  from 'react-router-dom'
const TeamDetails=()=>{
  
    const[team ,setTeam]= useState(
        {
            "id": 0,
            "name": "",
            "projectId": null,
            "project": {
             
            },
            "employees": [
              
            ]
          }
    );
    const {id}= useParams();
    
    useEffect(()=>{
        fetch(`http://localhost:44327/api/Team/team-details/${id}`)
        .then(res=> res.json())
        .then(res=>{
            setTeam(res);
        })
    },[])

    return(
        <div className='container mt-2'>
            <div class="card"  >
            <div class="card-header" id="headingOne">
                <h5 class="mb-0">
                    <button class="btn btn-link" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        {team.name} 
                    </button>
                </h5>
            </div>

            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                <div class="card-body">
                  
                    <table class="table ">
                        <tbody> 
                            <tr>
                                <th >Name</th>
                                <th>Email</th>
                                <th>Phone</th>
                                <th>Designation</th>
                                
                            </tr>
                            {team.employees.map((ele,index)=>
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
        </div>
    )
}

export default TeamDetails;