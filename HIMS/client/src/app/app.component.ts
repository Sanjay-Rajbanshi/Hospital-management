import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AppointmentListComponent } from './components/appointment-list/appointment-list.component';
import { PatientListComponent } from './components/patient-list/patient-list.component';
import { StaffListComponent } from './components/staff-list/staff-list.component';
@Component({
  selector: 'app-root',
  standalone: true,
  styleUrls: ['./app.component.css'],
  imports: [CommonModule, AppointmentListComponent, PatientListComponent, StaffListComponent],
  template: `
    <h1>HIMS Dashboard</h1>
    <app-patient-list></app-patient-list>
    <app-staff-list></app-staff-list>
    <app-appointment-list></app-appointment-list>
  `
})
export class AppComponent {
  
  
}
