import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private apiUrl = 'https://localhost:7050/api/appointment';

  constructor(private http:HttpClient) { }
  getAppointment():Observable<any[]>{
    return this.http.get<any[]>(this.apiUrl);
  }
  createAppointment(appointment: any): Observable<any>{
    return this.http.post<any>(this.apiUrl, appointment);
  }
  getAppointmentById(id: string):Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/$(id)`);
  }
updateAppointment(id: string, appointment: any): Observable<any>{
  return this.http.put<any>(`${this.apiUrl}/${id}`, appointment);
}
deleteAppointment(id: string): Observable<any>{
  return this.http.delete<any>(`${this.apiUrl}/${id}`);
}
}
