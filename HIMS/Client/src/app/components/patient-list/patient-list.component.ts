import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../services/patient.service';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule],
  template: `<h2>Patients</h2>
    <ul>
      <li *ngFor="let patient of patients">
        {{ patient.id }} - {{ patient.name }} - {{ patient.address }}
      </li>
    </ul>`,
  styleUrl: './patient-list.component.css',
  templateUrl: './patient-list.component.html',
})
export class PatientListComponent {
  patients: any[] = [];

  constructor(private patientService: PatientService) {}
  ngOnInit(){
    this.patientService.getPatient().subscribe(data => this.patients =data);
  }
}
