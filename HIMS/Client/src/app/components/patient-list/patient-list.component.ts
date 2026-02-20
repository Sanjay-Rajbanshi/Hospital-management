import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient.service';
import {FormsModule} from '@angular/forms'
import { HttpClient, HttpClientModule } from '@angular/common/http';
@Component({
  selector: 'patient-list',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
 
  styleUrl: './patient-list.component.css',
  templateUrl: './patient-list.component.html',
})
export class PatientListComponent implements OnInit {
  patient ={
    name: '',
    address: '',
    phoneNumber: '',
    age: null as number| null,
    problem: '',
    password:'abss',
    dateOfBirth: new Date()
  };

  constructor(private patientService: PatientService) {}
  ngOnInit(): void{
    this.loadPatients();
  }
  loadPatients(){
    this.patientService.getPatient().subscribe(data =>{
      this.patients = data;
    })
  }
  patients: any[] = [];
  editIndex: number | null =null;
  addOrUpdatePatient(){
    if(this.editIndex ===null){
      //add patient
      this.patients.push({...this.patient});

    }
    else{
      //update patient
      this.patients[this.editIndex] = {...this.patient};
    }

    this.resetForm();
  }
  addPatient(){
    this.patient.password
    this.patientService.createPatient(this.patient).subscribe(res=>{
      alert(res)
    })
  }
editPatient(index: number){
  this.patient = {...this.patients[index]};
  this.editIndex = index;
}
deletePatient(index: number){
  this.patients.splice(index, 1);
}
resetForm(){
  // this.patient={
  //   name: '',
  //   address: '',
  //    phoneNumber: '',
  //     age: null,
  //     problem: ''

  //};
}
  
}
