import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PatientListComponent } from './components/patient-list/patient-list.component';
import { AppointmentListComponent } from './components/appointment-list/appointment-list.component';
import { StaffListComponent } from './components/staff-list/staff-list.component';

export const routes: Routes = [
    { path: '', component: HomeComponent }, // default route
  { path: 'patients', component: PatientListComponent },
  { path: 'appointments', component: AppointmentListComponent },
  { path: 'staff', component: StaffListComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
