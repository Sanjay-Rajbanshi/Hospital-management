import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StaffService {
  private apiUrl = 'https://localhost:7050/api/staff';

  constructor(private http: HttpClient) { }
  getStaffs(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  createStaff(staff: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, staff);
  }

  getStaffById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  updateStaff(id: string, staff: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, staff);
  }

  deleteStaff(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
