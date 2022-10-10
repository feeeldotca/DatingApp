import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './models/user.modle'
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ServiceService implements OnInit{
  
  constructor(private http:HttpClient) { }
  //users: any; //User[]=[];
  title = 'The Dating App'; 
  ngOnInit(): void {
    
    this.getUsers();
    //throw new Error('Method not implemented.');
  }
  getUsers(): Observable<User[]> {
    
      return this.http.get<User[]>("https://localhost:5001/api/users");
      //.subscribe(response=>{
      //  this.users=response;
      //},       error => {        console.log(error);      })
    }
}
