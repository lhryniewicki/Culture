
import React from 'react';


class SearchWidget extends React.Component {

    render() {
        return (


            <div className="card my-4">
                <h5 className="card-header">Szukaj</h5>
                <div className="card-body">
                    <div className="input-group">
                        <input type="text" className="form-control" placeholder="Szukaj..."/>
                        <span className="input-group-btn">
                            <button className="btn btn-secondary" type="button">Wyszukaj!</button>
                        </span>
                    </div>
                </div>
            </div>
        );
    }
}
export default SearchWidget;



