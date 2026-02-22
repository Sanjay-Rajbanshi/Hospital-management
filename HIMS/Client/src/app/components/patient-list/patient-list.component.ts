import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient.service';
import {ReactiveFormsModule, FormBuilder, FormGroup, Validators} from '@angular/forms'
import {AgGridModule} from 'ag-grid-angular'
import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';
import { HttpClientModule } from '@angular/common/http';

ModuleRegistry.registerModules([AllCommunityModule]);

@Component({
  selector: 'patient-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, AgGridModule, ],
 
  styleUrl: './patient-list.component.css',
  templateUrl: './patient-list.component.html',
})
export class PatientListComponent implements OnInit {
  patients: any[]= [];
  columnDefs = [
  { headerName: 'Id', field: 'id' },
  { headerName: 'Name', field: 'name' },
  { headerName: 'Address', field: 'address' },
  { headerName: 'Phone', field: 'phoneNumber' },
  { headerName: 'DOB', field: 'dateOfBirth' },
  { headerName: 'Gender', field: 'gender' },
  {
    headerName: 'Actions',
    cellRenderer: (params: any) => {
     const container = document.createElement('div');
     const editBtn = document.createElement('button');
     editBtn.innerText = 'Edit';
     editBtn.className = 'btn btn-primary btn-sm me-2';
     editBtn.addEventListener('click', ()=>{
      params.context.componentParent.editPatient(params.data);
     });


     const deleteBtn = document.createElement('button');
     deleteBtn.innerText = 'Delete';
     deleteBtn.className = 'btn btn-danger btn-sm';
     deleteBtn.addEventListener('click', ()=>
    {
      params.context.componentParent.deletePatient(params.data.id);
    });


    container.appendChild(editBtn);
    container.appendChild(deleteBtn);
    return container;
    }
    
  }
];

  
  patientForm!: FormGroup;
  selectedPatientid: string | null = null;

  constructor(private patientService: PatientService, 
    private fb: FormBuilder
  ) {}
  ngOnInit(): void{
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      address: [''],
        phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required]
    });
    this.loadPatients();
  }
  
  onSubmit() {

  if (this.patientForm.invalid) {
    this.patientForm.markAllAsTouched();
    return;  // 🚨 STOP here if invalid
  }

  // Only runs if form is valid
  this.patientService.createPatient(this.patientForm.value)
    .subscribe(() => {
      this.loadPatients();
    });
}


  loadPatients(){
    this.patientService.getPatient().subscribe(data =>{
      this.patients = data;
    })
  }
  addPatient(){
    this.patientService.createPatient(this.patientForm.value).subscribe({
      next: ()=>{
        alert("Patient created successfully");
        this.loadPatients();
        this.resetForm();
      },
      error: (err)=>console.error(err)
    });
  }
  editPatient(patient: any){
    this.selectedPatientid= patient.id;
    this.patientForm.patchValue({
      name: patient.name,
      address: patient.address,
      phoneNumber: patient.phoneNumber,
      dateOfBirth: patient.dateOfBirth?.substring(0,10),
      gender: patient.gender
    });
  }
  updatePatient(){
    if(!this.selectedPatientid) 
      return;
    this.patientService.updatePatient(this.selectedPatientid, this.patientForm.value)
    .subscribe({
      next: (res: any) =>{
        alert(res.message);
        this.loadPatients();
        this.resetForm();

      },
      error: (err) => console.error(err)
    });
  }
  deletePatient(id: string){
    if (confirm("Are you sure want to delete?")){
      this.patientService.deletePatient(id)
      .subscribe({
        next: (res: any) =>{
          alert(res.message);
          this.loadPatients();
        //this.patients = this.patients.filter(p => p.id !== id);
        },
        error: (err) =>console.error(err)

      });
    }
  }
resetForm(){
  this.patientForm.reset();
  this.selectedPatientid = null;
}

}
