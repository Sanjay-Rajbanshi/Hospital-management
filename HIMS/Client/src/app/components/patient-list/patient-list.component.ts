import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient.service';
import {ReactiveFormsModule, FormBuilder, FormGroup, Validators} from '@angular/forms'
import {AgGridModule} from 'ag-grid-angular'
import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';
import { HttpClientModule } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
ModuleRegistry.registerModules([AllCommunityModule]);
declare var bootstrap: any;
@Component({
  selector: 'patient-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, AgGridModule, ],
 
  styleUrl: './patient-list.component.css',
  templateUrl: './patient-list.component.html',
})
export class PatientListComponent implements OnInit {
  patients: any[]= [];
  
  ngOnInit(): void{
    this.patientForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
        phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      dateOfBirth: ['', Validators.required],
      gender: ['', Validators.required],

      age: [{value: '', disabled: true}]

    });
    this.patientForm.get('dateOfBirth')?.valueChanges.subscribe(dob=>{
      if (dob){
        const age =this.calculateAge(dob);
        this.patientForm.patchValue({age:age});
      }
    });
    this.loadPatients();
  }
  
  columnDefs = [
  { headerName: 'Id', field: 'id' },
  { headerName: 'Name', field: 'name' },
  { headerName: 'Address', field: 'address' },
  // { headerName: 'Phone', field: 'phoneNumber' },
  // { headerName: 'DOB', field: 'dateOfBirth' },

  {
    headerName: 'Age',
    valueGetter: (params: any)=>{
      const dob = params.data.dateOfBirth;
      if(!dob) return '';
      return this.calculateAge(dob);
    }
  },

  // { headerName: 'Gender', field: 'gender' },
 
  {
  headerName: 'Actions',
  field: 'actions',
  cellRenderer: () => {
    return `
      <button class="btn btn-info btn-sm view-btn">View</button>
      <button class="btn btn-primary btn-sm edit-btn">Edit</button>
      <button class="btn btn-danger btn-sm delete-btn">Delete</button>
    `;
  },
  onCellClicked: (params: any) => {

    if (params.event.target.classList.contains('view-btn')) {
      this.viewPatient(params.data);
    }

    if (params.event.target.classList.contains('edit-btn')) {
      this.editPatient(params.data);
    }

    if (params.event.target.classList.contains('delete-btn')) {
      this.deletePatient(params.data.id);
    }
  }
}
];
selectedPatient: any = null;
isViewMode = false;

viewPatient(patient: any){
  this.selectedPatient= patient;
  this.isViewMode= true;
  
  
}
closeViewModal(){
  this.isViewMode = false;
  this.selectedPatient =null
}
  
  patientForm!: FormGroup;
  selectedPatientid: string | null = null;

  constructor(
    private patientService: PatientService, 
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) {}
  


  calculateAge(dob: string): number{
    const birthDate = new Date(dob);
    const today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();

    if (
      monthDiff < 0 ||
      (monthDiff === 0 && today.getDate() <birthDate.getDate())
    ){
      age--;
    }
    return age;
  } 
  
  onSubmit() {

  if (this.patientForm.invalid) {
    this.patientForm.markAllAsTouched();
    return;  //  STOP here if invalid
  }

  // Only runs if form is valid
  this.patientService.createPatient(this.patientForm.value)
    .subscribe(() => {
      this.loadPatients();
    });
}


  loadPatients(){
    this.patientService.getPatient().subscribe(data =>{
      this.patients = [...data];
      
    });
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
    this.openModal();
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
        this.selectedPatientid = null;
        this.isViewMode = false;
       

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
          this.selectedPatient = null;
          this.isViewMode = false;
          
        
        },
        error: (err) =>console.error(err)

      });
    }
  }
  cancelAction(){
    this.patientForm.reset();
    this.selectedPatientid= null;
  }
  

modalInstance: any;
openModal() {
  const modalEl = document.getElementById('patientModal');
  if (!this.modalInstance) {
    this.modalInstance = new bootstrap.Modal(modalEl);
  }
  this.modalInstance.show();
}

closeModal() {
  if (this.modalInstance) {
    this.modalInstance.hide();
  }
}

resetForm(){
  this.patientForm.reset();
  this.selectedPatientid = null;
}

}
