import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
private apiUrl = 'https://localhost:7050/api/patient';
  constructor(private http: HttpClient) { }
  getPatient():Observable<any[]>{
    return this.http.get<any[]>(this.apiUrl);
  }
  createPatient(patient: any): Observable<any>{
  let Url = 'https://localhost:7050/api/patient/createpatient';
    return this.http.post<any>(Url, patient);
  }
  getPatientById(id: string): Observable<any>{
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
  updatePatient(id: string, patient: any): Observable<any>{
    return this.http.put<any>(`${this.apiUrl}/${id}`, patient);
  }
  deletePatient(id: string, patient:any):Observable<any>{
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
