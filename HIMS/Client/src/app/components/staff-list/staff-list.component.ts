import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StaffService } from '../../services/staff.service';

@Component({
  selector: 'app-staff-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './staff-list.component.html',
  styleUrl: './staff-list.component.css',
 
})
export class StaffListComponent {
staffs: any[] = [];

  constructor(private staffService: StaffService) {}

  ngOnInit() {
    this.staffService.getStaffs().subscribe(data => this.staffs = data);
  }
}
