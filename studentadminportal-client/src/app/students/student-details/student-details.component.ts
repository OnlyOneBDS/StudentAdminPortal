import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

import { Gender } from 'src/app/models/ui/gender.model';
import { Student } from 'src/app/models/ui/student.model';
import { StudentService } from 'src/app/services/student.service';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.scss']
})
export class StudentDetailsComponent implements OnInit {
  genders: Gender[] = [];
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

  constructor(private studentService: StudentService, private route: ActivatedRoute, private snackBar: MatSnackBar, private router: Router) { }

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
              next: (student) => {
                this.student = student;
              },
              error: (error) => {
                console.log(error);
              }
            });

          this.studentService
            .getGenders()
            .subscribe({
              next: (genders) => {
                this.genders = genders
              },
              error: (error) => {
                console.log(error);
              }
            });
        }
      })
  }

  onUpdate(): void {
    this.studentService
      .updateStudent(this.student.id, this.student)
      .subscribe({
        next: (resp) => {
          // Show a notification
          this.snackBar.open("Student info updated!", '', { duration: 3000 });
        },
        error: (error) => {
          console.log(error);
        }
      });
  }

  onDelete(): void {
    this.studentService
      .deleteStudent(this.student.id)
      .subscribe({
        next: (resp) => {
          this.snackBar.open("Student deleted successfully!", '', { duration: 3000 });

          setTimeout(() => {
            this.router.navigateByUrl('/');
          }, 2000);
        },
        error: (error) => {
          console.log(error);
        }
      })
  }
}
