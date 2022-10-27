import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { Student } from '../models/ui/student.model';
import { StudentService } from '../services/student.service';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.scss']
})
export class StudentsComponent implements OnInit {
  displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'email', 'mobile', 'gender', 'edit'];
  students: Student[] = [];
  dataSource: MatTableDataSource<Student> = new MatTableDataSource<Student>();
  filterString = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private studentService: StudentService) { }

  ngOnInit(): void {
    this.getStudents();
  }

  getStudents() {
    this.studentService.getStudents()
      .subscribe({
        next: (success) => {
          this.students = success;
          this.dataSource = new MatTableDataSource<Student>(this.students);

          if (this.paginator) {
            this.dataSource.paginator = this.paginator;
          }

          if (this.sort) {
            this.dataSource.sort = this.sort;
          }
        },
        error: (error) => { console.log(error) }
      });
  }

  filterStudents() {
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }
}
