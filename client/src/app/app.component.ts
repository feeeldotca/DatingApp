import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './_service/account.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  users: any;
  title: string = "The Dating App";
  constructor(private http: HttpClient, private accountService: AccountService){}
  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
    }

    setCurrentUser() {
      const user: User = JSON.parse(localStorage.getItem('user')+"" );
      this.accountService.setCurrentUser(user);   
     
    }

    getUsers() {
      this.http.get('https://localhost:5001/api/users').subscribe(response => {
        this.users = response;
      })
    }

  }
  
