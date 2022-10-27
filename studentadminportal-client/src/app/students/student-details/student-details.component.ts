import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Student } from 'src/app/models/ui/student.model';
import { StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.scss']
})
export class StudentDetailsComponent implements OnInit {
  studentId: string | null | undefined;
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    imageUrl: '',
    gender: {
      id: '',
      description: ''
    },
    address: {
      id: '',
      physicalAddress: '',
      mailingAddress: ''
    }
  };

  constructor(private studentService: StudentService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getStudent();
  }

  getStudent() {
    this.route.paramMap
      .subscribe((params) => {
        this.studentId = params.get('id');

        if (this.studentId) {
          this.studentService
            .getStudent(this.studentId)
            .subscribe({
              next: (success) => {
                this.student = success;
              },
              error: (error) => {
                console.log(error);
              }
            });
        }
      })
  }
}
