import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = `${environment.apiUrl}qrcode`; // Replace with your API URL

  constructor(private http: HttpClient) { }

  post(data):  Observable<any>{
    return this.http.post(this.apiUrl, data, { responseType: 'blob'} );
  }
  get(data): Observable<any>{
    return this.http.get(this.apiUrl, { responseType: 'blob' } );
                            
  }
}
