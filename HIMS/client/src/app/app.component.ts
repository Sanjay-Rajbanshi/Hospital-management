import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AppointmentListComponent } from './components/appointment-list/appointment-list.component';
import { PatientListComponent } from './components/patient-list/patient-list.component';
import { StaffListComponent } from './components/staff-list/staff-list.component';
@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [CommonModule,HttpClientModule, AppointmentListComponent, PatientListComponent, StaffListComponent]
  })
export class AppComponent {
  
  
}
