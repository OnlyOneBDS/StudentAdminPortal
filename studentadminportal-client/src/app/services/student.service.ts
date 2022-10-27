import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Student } from '../models/api/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = 'https://localhost:7093/api/';

  constructor(private httpClient: HttpClient) { }

  getStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.baseUrl + 'students');
  }
}
