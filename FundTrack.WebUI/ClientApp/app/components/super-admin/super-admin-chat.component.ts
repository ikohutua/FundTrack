import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { SignalR, BroadcastEventListener, SignalRConnection, ConnectionStatus } from 'ng2-signalr'
import { ChatMessage } from '../../view-models/concrete/chat-message-view-model';

@Component({
    selector: 'super-admin-chat',
    template: require('./super-admin-chat.component.html')
})

/**
* Component for super admin chat 
*/
export class SuperAdminChatComponent implements OnInit { 
    // test variable for messages
    private _counter: number = 1;

    // signalr connection
    private _connection: SignalRConnection;

    // array for chat messages
    private _chatMessages: ChatMessage[] = [];

    /**
     * Creates new instance of SuperAdminChatComponent
     * @param _route
     */
    public constructor(private _route: ActivatedRoute) {
        this._connection = this._route.snapshot.data['connection'];
    }

    /**
     * Trigers when the component is created
     */
    public ngOnInit() {
        let onMessageSent$ = new BroadcastEventListener<ChatMessage>('OnMessageSent');

        this._connection.listen(onMessageSent$);

        onMessageSent$.subscribe((chatMessage: ChatMessage) => {
            this._chatMessages.push(chatMessage);
        });

        this._connection.start();  
    }

    /**
     * Sends message to server
     */
    public sendMessage() {
        this._connection.invoke('Chat', new ChatMessage('Message' +  this._counter.toString()));
        this._counter = this._counter + 1;
    }    
}