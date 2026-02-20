import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentService } from '../../services/appointment.service';
 

@Component({
  selector: 'app-appointment-list',
  standalone: true,
  imports: [CommonModule],
  
  styleUrl: './appointment-list.component.css',
  templateUrl: './appointment-list.component.html',
})
export class AppointmentListComponent {
appointments: any[] = [];
constructor(private appointmentService: AppointmentService){}
ngOnInit(){
  this.appointmentService.getAppointment().subscribe(data =>this.appointments = data);
}
}
