import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
import { FormGroup, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AgGridModule } from 'ag-grid-angular';
 import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';
import { PatientService } from '../../services/patient.service';
import { StaffService } from '../../services/staff.service';

declare var bootstrap: any;
@Component({
  selector: 'app-appointment-list',
  standalone: true,
  imports: [CommonModule, AgGridModule, ReactiveFormsModule],
  
  styleUrl: './appointment-list.component.css',
  templateUrl: './appointment-list.component.html',
})
export class AppointmentListComponent {

  minDateTime = new Date().toISOString().slice(0,16)

appointmentForm!: FormGroup;
patients: any[]= [];
doctors: any[]= [];
appointments: any[] = [];
rowData: any[]= [];
statuses = [
  {value : 0, label: 'Booked'},
  {value : 1, label: 'Completed'},
  {value: 2, label: 'Cancelled'}
];
columnsDefs = [
  {headerName: 'Appointment Id', field: 'id'},
  {headerName: 'Patient Name', field:'patientName'},
  {headerName: 'Doctor Name', field:'doctorName'},
  {headerName: 'Appointment DateTime', field:'appointmentDateTime'},
  {
    headerName: 'Status',
    field:'appointmentStatus',
    cellRenderer:(params: any) =>{
      switch(params.value){
        case 0: return  `<span class="badge bg-primary"> Booked</span>`;
        case 2: return `<span class="badge bg-danger">Cancelled</span>`;
        case 1: return `<span class="badge bg-success">Completed</span>`;
        default: return ''
      }
      
    }
  },
  {headerName: 'Actions',
    cellRenderer:()=>`
    <button class="btn btn-danger btn-sm cancel-btn">Cancel</button>
    `,
    onCellClicked: (params: any)=>{
      if(params.event.target.classList.contains('cancel-btn')){
        this.cancelAppointment(params.data.id);
      }
    }
  }
]

constructor(private appointmentService: AppointmentService,
  private fb: FormBuilder,
  private staffService: StaffService,
  private patientService: PatientService
){}
ngOnInit(): void{
  //this.appointmentService.getAppointment().subscribe(data =>this.appointments = data);
this.appointmentForm = this.fb.group({
  patientId: ['', Validators.required],
  doctorId: ['', Validators.required],
  appointmentDateTime: ['', Validators.required],
  status: [0]
});
this.loadPatients();
this.loadDoctors();
this.loadAppointments();
}
loadDoctors(){
  this.staffService.getStaffs().subscribe(data=>{
    this.doctors = data.filter((s:any)=>s.role===0);

  });
}
loadPatients(){
  this.patientService.getPatient().subscribe(data =>{
    this.patients = data;
  })
}

loadAppointments(){
  this.appointmentService.getAppointment().subscribe(data =>{
    this.appointments = data;
  });
}
addAppointments(){
    console.log(this.appointmentForm.value);
  console.log(this.appointmentForm.valid);
  console.log(this.appointmentForm.controls);
  if(this.appointmentForm.invalid){
    alert("please fill all the fields");
    return;
  }
  this.appointmentService.createAppointment(this.appointmentForm.value).subscribe({
    next: ()=>{
      alert ('Appointment is booked');
      this.loadAppointments();
      this.resetForm();

    },
    error: (err)=>{
      console.error(err);
      alert("Error creating appointment");

    }

  });
}
cancelAppointment(id:string){
   this.appointmentService.cancelAppointment(id).subscribe(()=>{
    alert('Appointment Cancelled');
    this.loadAppointments();
  })
}
resetForm(){
  this.appointmentForm.reset();

}

modalInstance: any;
openModal(){
  const modalElement = document.getElementById('appointmentModal');
  if(!this.modalInstance){
    this.modalInstance = new bootstrap.Modal(modalElement)
  }
  this.modalInstance.show();
}
closeModal(){
  if(this.modalInstance){
    this.modalInstance.hide();
  }
}
}
