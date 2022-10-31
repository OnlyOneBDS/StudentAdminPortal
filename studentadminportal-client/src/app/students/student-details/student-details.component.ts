import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
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
  header = '';
  displayImageUrl = '';
  isNewStudent = false;
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

  @ViewChild('studentForm') studentForm?: NgForm;

  constructor(private studentService: StudentService, private route: ActivatedRoute, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {
    this.getStudent();
  }

  getStudent() {
    this.route.paramMap
      .subscribe((params) => {
        this.studentId = params.get('id');

        if (this.studentId) {
          // If the route contains 'Add'
          // -> new Studnent
          if (this.studentId.toLowerCase() === 'Add'.toLowerCase()) {
            this.isNewStudent = true;
            this.header = 'Add New Student';
            this.setImage();
          }
          else {
            this.isNewStudent = false;
            this.header = 'Edit Student';
            this.studentService
              .getStudent(this.studentId)
              .subscribe({
                next: (student) => {
                  this.student = student;
                  this.setImage();
                },
                error: (error) => {
                  console.log(error);
                  this.setImage();
                }
              });
          }

          this.studentService
            .getGenders()
            .subscribe({
              next: (genders) => {
                this.genders = genders;
                this.setImage();
              },
              error: (error) => {
                console.log(error);
              }
            });
        }
      })
  }

  onAdd(): void {
    if (this.studentForm?.form.valid) {
      this.studentService
        .addStudent(this.student)
        .subscribe({
          next: (resp) => {
            this.snackBar.open("Student added successfully!", '', { duration: 3000 });

            setTimeout(() => {
              this.router.navigateByUrl(`students/${resp.id}`);
            }, 2000);
          },
          error: (error) => {
            console.log(error);
          }
        });
    }
  }

  onUpdate(): void {
    if (this.studentForm?.form.valid) {
      this.studentService
        .updateStudent(this.student.id, this.student)
        .subscribe({
          next: () => {
            // Show a notification
            this.snackBar.open("Student info updated!", '', { duration: 3000 });
          },
          error: (error) => {
            console.log(error);
          }
        });
    }
  }

  onDelete(): void {
    this.studentService
      .deleteStudent(this.student.id)
      .subscribe({
        next: () => {
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

  uploadImage(event: any): void {
    if (this.studentId) {
      const file: File = event.target.files[0];

      this.studentService
        .uploadImage(this.student.id, file)
        .subscribe({
          next: (resp) => {
            console.log(resp);
            this.student.imageUrl = resp;
            this.setImage();
            this.snackBar.open('Image updated successfully!', '', { duration: 3000 });
          },
          error: (error) => {
            console.log(error);
          }
        });
    }
  }

  private setImage(): void {
    if (this.student.imageUrl) {
      this.displayImageUrl = this.studentService.getImage(this.student.imageUrl);
    }
    else {
      this.displayImageUrl = '/assets/user.png';
    }
  }
}
