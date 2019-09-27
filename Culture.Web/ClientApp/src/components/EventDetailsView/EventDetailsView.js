﻿import React from 'react';
import CommReactionBar from '../CommReactionBar/CommReactionBar';
import { getEventDetails } from '../../api/EventApi';
import { addToCalendar, removeFromCalendar, signUser, unsignUser } from '../../api/AttendanceApi';
import '../EventDetailsView/EventDetailsView.css';


class EventDetailsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            name: "",
            isInCalendar: false,
            isSigned: false,
            id: 0,
            price: 0,
            createdBy: "",
            urlSlug: this.props.match.params.eventSlug,
            category: "",
            reactions: [],
            comments: [],
            date: null,
            imagePath: null,
            isPreview: false,
            commentsCount: 0,
            reactionsCount: 0,
            canLoadMore: false,
            currentReaction: null,
            cityName: "",
            streetName: "",
            takesPlaceHour: "00:00",
            takesPlaceDate: null
        };

    }

    DisplayCalendarButton = () => {
        if (this.state.isInCalendar) return "Usuń z kalendarza";
        else {
            return "Dodaj do kalendarza"
        }
    }

    DisplayAttendButton = () => {
        if (this.state.isSigned) return "Wypisz się";
        else {
            return "Biorę udział"
        }
    }

    SignUser = async () => {

        if (this.state.isSigned) {
            await unsignUser(this.state.id);
            this.setState({
                isSigned: false
            })
        }
        else {
            await signUser(this.state.id);
            this.setState({
                isSigned: true
            });
        }
    }

    HandleCalendar = async () => {
        if (this.state.isInCalendar) {
            await removeFromCalendar(this.state.id);
            this.setState({
                isInCalendar: false
            })
        }
        else {
            await addToCalendar(this.state.id);
            this.setState({
                isInCalendar: true
            });
        }
    }
    async componentDidMount() {
        const result = await getEventDetails(this.state.urlSlug);
        console.log(result);
        const jsDate = new Date(Date.parse(result.creationDate));
        const jsDateFormatted = jsDate.getUTCDate() + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear();

        const jsDatePlace = new Date(Date.parse(result.creationDate));
        const jsDatePlaceFormatted = jsDatePlace.getUTCDate() + "-" + (jsDatePlace.getMonth() + 1) + "-" + jsDatePlace.getFullYear();
        const jsDatePlaceTimeFormatted = jsDatePlace.getHours() + ":" + jsDatePlace.getMinutes();

        this.setState({
            canLoadMore: result.canLoadMore,
            category: result.category,
            name: result.name,
            id: result.id,
            cityName: result.cityName,
            imagePath: result.image,
            reactions: result.reactions,
            streetName: result.streetName,
            comments: result.comments,
            commentsCount: result.commentsCount,
            reactionsCount: result.reactionsCount,
            currentReaction: result.currentReaction,
            content: result.content,
            createdBy: result.createdBy,
            date: jsDateFormatted,
            price: result.price,
            takesPlaceDate: jsDatePlaceFormatted,
            takesPlaceHour: jsDatePlaceTimeFormatted,
            isInCalendar: result.isInCalendar,
            isSigned: result.isUserSigned

        });
    }
    render() {
        return (

            <div className="container pt-5">
               
                
                <div className="row " >
                    <div className="col-md-8 h-100 " >
                        <div className="card">
                            <div className="card-header ">
                                <img className="img-fluid pull-right mt-4" width="730" height="615" src={this.state.imagePath} />
                        <div className=" mt-4">
                            <div className="card">
                                <div className="card-body">
                                    {this.state.content}
                                </div>
                                <CommReactionBar
                                    currentReaction={this.state.currentReaction}
                                    id={this.state.id}
                                    createdBy={this.state.createdBy}
                                    reactions={this.state.reactions}
                                    reactionsCount={this.state.reactionsCount}
                                    date={this.state.date}
                                    comments={this.state.comments}
                                    commentsCount={this.state.commentsCount}
                                    currentReaction={this.state.currentReaction}
                                    canLoadMore={this.state.canLoadMore}
                                    isPreview={this.state.isPreview} />
                            </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="col-md-4">
                        <div className="card ">
                            <div className="card-header text-center">
                                <div className="text-center mb-3">
                                    <b>Informacje ogólne</b>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Nazwa
                                     </div>
                                    <div className="card-body">
                                        <b>{this.state.name}</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Data odbycia
                                     </div>
                                    <div className="card-body">
                                        Dnia <b> {this.state.takesPlaceDate}</b> godz. {this.state.takesPlaceHour}
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Adres
                                </div>
                                    <div className="card-body">
                                        <b> {this.state.streetName},  {this.state.cityName}</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Kategoria
                                </div>
                                    <div className="card-body">
                                        <b> {this.state.category}</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <div className="card-header ">
                                        Cena
                                </div>
                                    <div className="card-body">
                                        <b> {this.state.price}</b>
                                    </div>
                                </div>

                            </div>
                            <div className="card-body mb-0">
                                <div className="row mb-3 ">
                                    <div className="mx-auto">
                                        <button onClick={this.HandleCalendar} className="btn btn-danger">{this.DisplayCalendarButton()}</button>
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="mx-auto ">
                                        <button onClick={this.SignUser} className="btn btn-primary">{this.DisplayAttendButton()} </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="card mt-4">
                            <div className="card-header text-center">
                                <div className="text-center mb-3">
                                    <b>Podobne wydarzenia</b>
                                </div>
                                <div className="card mb-3">
                                    <img src="http://placehold.it/200x100" className="card-img-top" />
                                    <div className="card-header ">
                                        <b> Progress Days - warsztaty z certyfikatem</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <img src="http://placehold.it/200x100" className="card-img-top" />
                                    <div className="card-header ">
                                        <b> Progress Days - warsztaty z certyfikatem</b>
                                    </div>
                                </div>
                                <div className="card mb-3">
                                    <img src="http://placehold.it/200x100" className="card-img-top" />
                                    <div className="card-header ">
                                        <b> Progress Days - warsztaty z certyfikatem</b>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>

                <div className="row ">
                    <div className="col-md-4 mt-4">
                       
                    </div>
                </div>
            </div>
        );
    }
}
export default EventDetailsView;
