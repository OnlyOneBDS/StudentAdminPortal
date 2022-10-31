import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AddStudent } from '../models/api/add-student.model';
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

  addStudent(student: Student): Observable<Student> {
    const studentToAdd: AddStudent = {
      firstName: student.firstName,
      lastName: student.lastName,
      dateOfBirth: student.dateOfBirth,
      email: student.email,
      mobile: student.mobile,
      genderId: student.genderId,
      physicalAddress: student.address.physicalAddress,
      mailingAddress: student.address.mailingAddress,
    };

    return this.httpClient.post<Student>(this.baseUrl + '/students', studentToAdd);
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
    };

    return this.httpClient.put<Student>(this.baseUrl + '/students/' + studentId, studentToUpdate);
  }

  deleteStudent(studentId: string): Observable<Student> {
    return this.httpClient.delete<Student>(this.baseUrl + '/students/' + studentId);
  }

  getGenders(): Observable<Gender[]> {
    return this.httpClient.get<Gender[]>(this.baseUrl + '/students/genders');
  }

  uploadImage(studentId: string, file: File): Observable<any> {
    const formData = new FormData();

    formData.append("imageFile", file);

    return this.httpClient.post(this.baseUrl + '/students/upload-image/' + studentId, formData, { responseType: 'text' });
  }

  getImage(imageUrl: string) {
    return `${this.baseUrl}/${imageUrl}`;
  }
}
