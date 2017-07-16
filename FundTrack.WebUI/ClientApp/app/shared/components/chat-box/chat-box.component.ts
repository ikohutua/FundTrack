//import { Component, Output, EventEmitter, OnInit } from "@angular/core";
////import { SignalR, BroadcastEventListener, SignalRConnection, ConnectionStatus } from 'ng2-signalr'
//import { ActivatedRoute } from "@angular/router";
//import { ChatMessage } from "../../../view-models/concrete/chat-message-view-model";
//import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";

//@Component({
//    selector: 'chat-box',
//    templateUrl: './chat-box.component.html',
//    styleUrls: ['./chat-box.component.css']
//})

///**
//* Component for Chat window
//*/
//export class ChatBoxComponent implements OnInit {    
//    // signalr connection
//    private _connection: SignalRConnection;

//    // array for chat messages
//    private _chatMessages: ChatMessage[] = [];

//    // current text in textbox
//    private _currentMessage: string = '';

//    /**
//     * Creates new instance of SuperAdminChatComponent
//     * @param _route
//     */
//    public constructor(private _route: ActivatedRoute) {
//        this._connection = this._route.snapshot.data['connection'];
//    }

//    /**
//     * Trigers when the component is created
//     */
//    public ngOnInit() {
//        let onMessageSent$ = new BroadcastEventListener<ChatMessage>('OnMessageSent');

//        this._connection.listen(onMessageSent$);

//        onMessageSent$.subscribe((chatMessage: ChatMessage) => {
//            this._chatMessages.push(chatMessage);
//        });

//        this._connection.start();
//    }

//    /**
//     * Sends message to server
//     */
//    public sendMessage() {
//        console.log(this._currentMessage);

//        let user = JSON.parse(localStorage.getItem('model')) as AuthorizeUserModel;

//        console.log(user);

//        console.log(this._connection.id);

//        let test = this._connection.id;

//        this._connection.invoke('Chat', new ChatMessage(this._currentMessage, test, user.login));
//    }
//}