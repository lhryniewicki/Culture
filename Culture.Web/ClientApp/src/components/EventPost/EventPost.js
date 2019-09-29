﻿import React from 'react';
import { Link } from 'react-router-dom';
import '../EventPost/EventPost.css';
import CommReactionBar from '../CommReactionBar/CommReactionBar';

class EventPost extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            source: null,
            id: this.props.id,
            urlSlug: this.props.urlSlug
        };
    }
    componentDidMount() {
        if (this.props.isPreview) {
            this.props.picture.name === 'Wybierz plik' ? this.setState({ source: "https://via.placeholder.com/750x300" })
                : this.setState({ source:URL.createObjectURL(this.props.picture)});
        }
        else {
            this.setState({ source: this.props.picture });
        }
        
    }
    render() {
        return (
            <div className="card my-4">
                <img className="card-img-top"
                    width="750px;"
                    height="300px;"
                    src={this.state.source}
                    alt="Brak zdjęcia!" />
                <div className="card-body">
                    <h2 className="card-title">{this.props.eventName}</h2>
                    <p className="card-text showSpace">{this.props.eventDescription}</p>
                    {this.props.isPreview === false ?
                        <Link to={`/wydarzenie/szczegoly/${this.state.urlSlug}`}><span className="btn btn-primary">Szczegóły... &rarr;</span></Link>
                        :
                        <span className="btn btn-primary">Szczegóły... &rarr;</span>
                    }
              
                </div>
                <CommReactionBar
                    currentReaction={this.props.currentReaction}
                    id={this.props.id}
                    createdBy={this.props.createdBy}
                    reactions={this.props.reactions}
                    reactionsCount={this.props.reactionsCount}
                    date={this.props.isPreview === false ? this.props.date : new Date().toLocaleDateString("en-Us")}
                    comments={this.props.comments}
                    commentsCount={this.props.commentsCount}
                    canLoadMore={this.props.canLoadMore}
                    isPreview={this.props.isPreview}
                />
            </div>


        );
    }
}
export default EventPost;
