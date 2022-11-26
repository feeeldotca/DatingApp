import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member' 

  @Injectable({
    providedIn: 'root'
  })
  
  export class MembersService {
    baseUrl = environment.apiUrl;
    
    constructor(private http: HttpClient) {}
    
    getMembers() {
      return this.http.get<Member[]>(this.baseUrl + 'users', this.getHttpOptions());
    }
  
    getMember(username: string) {
      return  this.http.get<Member>(this.baseUrl + 'users/' + username, this.getHttpOptions());
    }

    getHttpOptions() {
      const userString:any = localStorage.getItem('user');     
      if(!userString) return {}; 
      return {
        headers: new HttpHeaders({
          Authorization: 'Bearer ' + JSON.parse(userString).token
        })};      
    }
  }

