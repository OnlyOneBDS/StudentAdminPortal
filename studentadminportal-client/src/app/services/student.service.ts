import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Gender } from '../models/api/gender.model';
import { Student } from '../models/api/student.model';
import { UpdateStudent } from '../models/api/update-student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = 'https://localhost:7093/api';

  constructor(private httpClient: HttpClient) { }

  getStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.baseUrl + '/students');
  }

  getStudent(studentId: string): Observable<Student> {
    return this.httpClient.get<Student>(this.baseUrl + '/students/' + studentId);
  }

  updateStudent(studentId: string, student: Student): Observable<Student> {
    const studentToUpdate: UpdateStudent = {
      firstName: student.firstName,
      lastName: student.lastName,
      dateOfBirth: student.dateOfBirth,
      email: student.email,
      mobile: student.mobile,
      genderId: student.genderId,
      physicalAddress: student.address.physicalAddress,
      mailingAddress: student.address.mailingAddress,
    }

    return this.httpClient.put<Student>(this.baseUrl + '/students/' + studentId, studentToUpdate);
  }

  getGenders(): Observable<Gender[]> {
    return this.httpClient.get<Gender[]>(this.baseUrl + '/students/genders');
  }
}
